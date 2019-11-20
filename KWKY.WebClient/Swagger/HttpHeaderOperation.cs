using KWKY.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KWKY.WebClient.Swagger
{
    /// <summary>
    /// Swagger插件 需要验证的方法中加入head参数token,platformKey 
    /// 默认全都需要 token,platformKey  没有AllowAnonymousAttribute  则需要head参数token ,platformKey 
    /// </summary>
    public class HttpHeaderOperation : IOperationFilter
    {
        public void Apply (OpenApiOperation operation, OperationFilterContext context)
        {
            bool isAllowAnonymous = false;

            var methodInfo = context.MethodInfo;
            if ( methodInfo != null )
            {
                isAllowAnonymous = methodInfo.IsDefined(typeof(AllowAnonymousAttribute), true);
            }
            if ( !isAllowAnonymous )
            {
                isAllowAnonymous = methodInfo.DeclaringType.IsDefined(typeof(AllowAnonymousAttribute), true);
            }

            if ( !isAllowAnonymous )
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = ConstData.JwtTokenName,  //添加Authorization头部参数
                    In = ParameterLocation.Header,
                    Required = false
                });
            }
        }
    }
}
