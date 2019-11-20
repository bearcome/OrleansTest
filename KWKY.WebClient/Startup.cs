using KWKY.Common;
using KWKY.IGrains;
using KWKY.WebClient.AppCode;
using KWKY.WebClient.Attributes;
using KWKY.WebClient.Authentication;
using KWKY.WebClient.Filters;
using KWKY.WebClient.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[assembly: ApiController]
namespace KWKY.WebClient
{
    public class Startup
    {
        private const string JwtSettingsName = "JwtSettings";
        private readonly ILogger _logger;
        private JwtSettings jwtSettings;
        private JwtBearerEvents jwtBearerEvents ;
        private TokenValidationParameters tokenValidationParameters;

        public Startup (IConfiguration configuration)
        {
            LogManager.LoadConfiguration("Nlog.config");
            _logger = LogManager.GetCurrentClassLogger();
            Configuration = configuration;
            
            //HTTP/2 打开未加密的连接       http/2 比 http/1 高效 强大
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }

        public IConfiguration Configuration { get; }

  
        public void ConfigureServices (IServiceCollection services)
        {
            Init(services);

            services.AddMvc(options =>
            {
               
                options.RespectBrowserAcceptHeader = true;      // false by default  true 允许接受 header 参数  指定 Accept 标头时，会发生内容协商
                options.MaxModelValidationErrors = 50;
                options.RespectBrowserAcceptHeader = true;      //内容协商 默认false  请求头中Accept: application/xml 希望返回的类型
                options.SuppressInputFormatterBuffering = true;
                options.EnableEndpointRouting = false;

                //可按类型或实例添加筛选器。 如果添加实例，该实例将用于每个请求。 如果添加类型，则将激活该类型，这意味着将为每个请求创建一个实例，
                //并且依赖关系注入(DI) 将填充所有构造函数依赖项。 按类型添加筛选器等效于 
                options.Filters.Add(new AsyncAuthorizationFilter());
                //options.Filters.Add(new AsyncResourceFilter());
                options.Filters.Add(new AsyncGlobalExceptionFilter());
                //options.Filters.Add(new AsyncActionFilter());
                //全局模型验证
                options.Filters.Add(new AsyncModelVerfyFilter());

            })
            //MVC全局 Json序列化设置
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                opt.JsonSerializerOptions.IgnoreNullValues = false;
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Latest);

            //注册跨域服务
            services.AddCors(options =>
                 options.AddPolicy(ConstData.CorsPolicyName,
                builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
            
            //注册服务
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenValidator = new JwtTokenValidator(jwtSecurityTokenHandler);
            


            var client = GetClientAsync(services).Result;

            services.AddSingleton(typeof(IClusterClient), client);
            services.AddSingleton(typeof(JwtSecurityTokenHandler), jwtSecurityTokenHandler);
            services.AddSingleton(typeof(JwtTokenValidator), jwtTokenValidator);
            services.AddScoped<RouteService>();

            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //主要是jwt  token参数设置
                options.TokenValidationParameters = tokenValidationParameters;
                options.SecurityTokenValidators.Clear();//将SecurityTokenValidators清除掉，否则它会在里面拿验证
                options.SecurityTokenValidators.Add(jwtTokenValidator);
                options.Events = jwtBearerEvents;
            });

#if !RELEASE
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "KWKY接口文档",
                    Description = "RESTful API for KWKY",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Yangming", Email = "yangming@mdruby.com" }
                });

                //Set the comments path for the swagger json and ui.
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "KWKY.WebClient.xml");
                c.IncludeXmlComments(xmlPath, true);
                c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app,IWebHostEnvironment env)
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

#if !RELEASE
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KWKY API V1");
            });
#endif
            app.UseCors(ConstData.CorsPolicyName);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        /// <summary>
        /// 初始化部分数据
        /// </summary>
        private void Init (IServiceCollection services)
        {
            services.Configure<JwtSettings>(Configuration.GetSection(JwtSettingsName));
            jwtSettings = new JwtSettings();
            Configuration.Bind(JwtSettingsName, jwtSettings);
            services.AddSingleton(typeof(JwtSettings), jwtSettings);

            tokenValidationParameters = new TokenValidationParameters
            {
                //Token颁发机构
                ValidIssuer = jwtSettings.JwtIssuer,
                //颁发给谁
                ValidAudience = jwtSettings.JwtAudience,
                //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtSecretKey)),
                ValidateIssuerSigningKey = true,
                //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ValidateLifetime = true,
                ////允许的服务器时间偏移量
                ClockSkew = TimeSpan.Zero
            };

            jwtBearerEvents = new JwtBearerEvents
            {
                //重写OnMessageReceived
                OnMessageReceived = context =>
                {
                    var token = context.Request.Headers[ConstData.JwtTokenName];
                    context.Token = token.FirstOrDefault();
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    var claims = (context.SecurityToken as JwtSecurityToken)?.Claims;
                    var userId = claims?.FirstOrDefault(o => o.Type == ConstData.ClaimTypes_UserId)?.Value;
                    if ( !string.IsNullOrEmpty(userId) )
                    {
                        context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                        context.Success();
                    }
                    else
                    {
                        context.Fail("token is not validated");
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context=>
                {
                    return Task.CompletedTask;
                },
                OnChallenge = context=>
                {
                    return Task.CompletedTask;
                },
                OnForbidden = context=>
                {
                    return Task.CompletedTask;
                }
                 
            };
        }




        #region Orleans



        /// <summary>
        /// 启动Orleans客户端
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private async Task<IClusterClient> GetClientAsync (IServiceCollection services)
        {
            try
            {
#if     RELEASE || DEBUG
                return await StartClusterClient(services);
#elif   DEVELOPDEBUG
                return await StartLocalClient(services);
#endif

            }
            catch ( Exception e )
            {
                _logger.Error(e);
                return null;
            }
        }



        /// <summary>
        /// 启动集群客户端     Release|Debug 模式
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private async Task<IClusterClient> StartClusterClient (IServiceCollection services)
        {

            int siloCount = Configuration.GetSection("SiloSettings").GetValue<int>("SiloCount");

            IPEndPoint[] gateways = new IPEndPoint[siloCount];
            for ( int i = 0; i < siloCount; i++ )
            {
                string silo = string.Format("Silos:{0}", i);
                string ipAddr = Configuration.GetSection("SiloSettings").GetSection(silo).GetValue<string>("IpAddress");
                int gatewayPort = Configuration.GetSection("SiloSettings").GetSection(silo).GetValue<int>("GatewayPort");
                gateways[i] = new IPEndPoint(IPAddress.Parse(ipAddr), gatewayPort);
            }

            string orleansConnStr = Configuration.GetConnectionString("KWKYOrleans");

            IClusterClient client = new ClientBuilder()
                .UseStaticClustering(gateways)
#if RELEASE
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ConstData.ClusterId;
                    options.ServiceId = ConstData.ServiceId;
                })
#else
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ConstData.ClusterIdDev;
                    options.ServiceId = ConstData.ServiceIdDev;
                })
#endif
                
                .UseAdoNetClustering(options =>
                {
                    options.ConnectionString = orleansConnStr;
                    options.Invariant = ConstData.Invariant;
                })
#warning  可配置加载的程序集
                .ConfigureApplicationParts(parts=>parts.AddApplicationPart(typeof(IDefaultGrain).Assembly).WithReferences())
                .ConfigureLogging(builder => builder.AddNLog())
                //客户端注册异常过滤器
                .AddOutgoingGrainCallFilter(new AsyncGrainExceptionFilter())
                .Build();
            await client.Connect();
            _logger.Info("Client successfully connected to silo host \n");
            return client;
        }




        /// <summary>
        /// 启动本地客户端 Develop模式
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private async Task<IClusterClient> StartLocalClient (IServiceCollection services)
        {

            string orleansConnStr = Configuration.GetConnectionString("KWKYOrleans");

            IClusterClient client = new ClientBuilder()
                .UseLocalhostClustering (30000,ConstData.ServiceIdDev,ConstData.ClusterIdDev)
                .ConfigureApplicationParts(conf=>conf.AddApplicationPart(typeof(IDefaultGrain).Assembly).WithReferences())

                .ConfigureLogging(builder => builder.AddNLog())
                //客户端注册异常过滤器
                .AddOutgoingGrainCallFilter(new AsyncGrainExceptionFilter())
                .Build();
            await client.Connect();
            _logger.Info("Client successfully connected to silo host \n");
            return client;
        }


        #endregion



    }

   
}
