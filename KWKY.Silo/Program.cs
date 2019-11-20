using KWKY.Common;
using KWKY.DBUtility;
using KWKY.Grains;
using KWKY.Silo.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace KWKY.Silo
{
    class Program
    {
        static ISiloHost siloHost;
        static readonly ManualResetEvent _siloStopped = new ManualResetEvent(false);
        static bool siloStopping = false;
        static readonly object syncLock = new object();
        static ILogger _logger;
        static void Main (string[] args)
        {
            LogManager.LoadConfiguration("Nlog.config");
            _logger = LogManager.GetCurrentClassLogger();
            SetupApplicationShutdown();
            siloHost = StartSilo().Result;
            //阻止当前线程直到收到信号，不收到信号永不返回
            _siloStopped.WaitOne();
        }


#if RELEASE || DEBUG
        /// <summary>
        /// 启动silo  Release
        /// </summary>
        /// <returns></returns>
        private static async Task<ISiloHost> StartSilo ()
        {
            var orleansConnStr = AppConfiguration.GetConnectionString("KWKYOrleans"); 
            var siloIpAddress = AppConfiguration.GetIpAppSetting("LocalIpAddress");
            int siloPort = AppConfiguration.GetIntAppSetting("SiloPort");
            int gatewayPort = AppConfiguration.GetIntAppSetting("GatewayPort");
            int globalGrainCollectAgeMinute = AppConfiguration.GetIntAppSetting("GlobalGrainCollectAgeMinute");


            var builder = new SiloHostBuilder()
                .ConfigureEndpoints(siloIpAddress,  siloPort: siloPort, gatewayPort: gatewayPort)
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
               

#warning 做成配置  如果不配置则加载所有
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(DefaultGrain).Assembly).WithReferences())

                .ConfigureLogging(logging => logging.AddNLog())
                .UseAdoNetClustering(options =>
                {
                    options.ConnectionString = orleansConnStr;
                    options.Invariant = Common.ConstData.Invariant;
                })
                
                .AddAdoNetGrainStorage(ConstData.AdoNetGrainStorageName, options =>
                {
                    options.Invariant = Common.ConstData.Invariant;
                    options.ConnectionString = orleansConnStr;
                    options.UseJsonFormat = true;
                })
                //全局生命周期  
                .Configure<GrainCollectionOptions>(options =>
                {
                    options.CollectionAge = TimeSpan.FromMinutes(globalGrainCollectAgeMinute);
                    options.ClassSpecificCollectionAge[typeof(DefaultGrain).FullName]=TimeSpan.FromMinutes(5);
                })
                .ConfigureServices(services=>
                {
                    ConfigureServices(services);
                })
                //silo端注册异常转换过滤器  client 可能不识别Silo端异常类型 typeof(**Exception)
                .AddIncomingGrainCallFilter<ExceptionConversionFilter>()
                .AddStartupTask<StartUpTask>()
                .UseDashboard();
            var host = builder.Build();
            await host.StartAsync();
            return host;
        }

#else
        /// <summary>
        /// 启动silo   Develop
        /// </summary>
        /// <returns></returns>
        private static async Task<ISiloHost> StartSilo()
        {
            int globalGrainCollectAgeMinute = int.Parse(ConfigurationManager.AppSettings["GlobalGrainCollectAgeMinute"]);
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ConstData.ClusterId;
                    options.ServiceId = ConstData.ServiceId;
                })

                //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(EmptyFormGrain).Assembly).WithReferences())
                //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(EmptyDataIndexGrain).Assembly).WithReferences())
                //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(EmptyMetaFhirGrain).Assembly).WithReferences())
                //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(DefaultGrain).Assembly).WithReferences())
                .ConfigureApplicationParts(parts => LoadAssembly(parts))

                .ConfigureLogging(logging => logging.AddNLog())
                //全局生命周期  
                .Configure<GrainCollectionOptions>(options => 
                {
                    options.CollectionAge = TimeSpan.FromMinutes(globalGrainCollectAgeMinute);
                    options.ClassSpecificCollectionAge[typeof(DefaultGrain).FullName]=TimeSpan.FromMinutes(5);
                })
                .ConfigureServices(services=>
                {
                    ConfigureServices(services);
                })
                //silo端注册异常转换过滤器  client 可能不识别Silo端异常类型 typeof(**Exception)
                .AddIncomingGrainCallFilter<ExceptionConversionFilter>()
                .AddStartupTask<StartUpTask>()
                .UseDashboard();
            var host = builder.Build();
            await host.StartAsync();
            return host;
        }

#endif

        private static void ConfigureServices (IServiceCollection services)
        {
            services.AddDbContextPool<KWKYDBContext>(opt=> {
                opt.UseSqlServer(AppConfiguration.GetConnectionString("KWKYOrleans"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
#if !RELEASE    //Sql拦截  Sql警告
                opt.AddInterceptors(new SqlCommandInterceptor());
                opt.ConfigureWarnings(warningBuilder => warningBuilder.Log((RelationalEventId.CommandExecuting, Microsoft.Extensions.Logging.LogLevel.Debug)));
#endif
            });
            //services.AddSingleton<IDataServiceClient, DataServiceClient>();
            //services.AddHttpClient();
            //services.AddScoped<>();
            //services.AddSingleton();
            //services.AddTransient<>();
        }

        /// <summary>
        /// 设置关闭事件 
        /// 最后一个Silo关闭时会产生异常，官方认为是合理的
        /// Docker 暂不支持优雅关闭
        /// </summary>
        private static void SetupApplicationShutdown ()
        {
            /// C捕捉  Ctrl+C  事件
            Console.CancelKeyPress += (s, a) =>
            {
                _logger.Warn("\n\n This Silo is Terminating!!\n\n");
                /// 防止直接退出（不优雅）
                a.Cancel = true;
                /// 防止执行多次 Ctrl+C .
                lock ( syncLock )
                {
                    if ( !siloStopping )
                    {
                        siloStopping = true;
                        //执行退出Silo 忽略异常
                        Task.Run(StopSilo).Ignore();
                    }
                }
            };
        }

        /// <summary>
        /// 终止silo
        /// </summary>
        /// <returns></returns>
        private static async Task StopSilo ()
        {
            await siloHost.StopAsync();
            _siloStopped.Set();
            _logger.Warn("\n\n This Silo is Terminated!!\n\n");
        }

    }
}
