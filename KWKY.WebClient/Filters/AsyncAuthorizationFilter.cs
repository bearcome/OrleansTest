using KWKY.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace KWKY.WebClient.Filters
{
    /// <summary>
    /// 授权筛选器最先运行，用于确定是否已针对当前请求为当前用户授权。 如果请求未获授权，它们可以让管道短路。
    /// </summary>
    public class AsyncAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public AsyncAuthorizationFilter ()
        {

        }
        public async Task OnAuthorizationAsync (AuthorizationFilterContext context)
        {

            bool allowAnonymous = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), false);
            if ( allowAnonymous )
            {
                context.HttpContext.User = null;
                await Task.CompletedTask;
            }

            var user = context.HttpContext.User;

#warning 实现 user.IsRequestValid()  CustomizedResorceAttribute
            if ( user == null )//
            {
                context.Result = new JsonResult(ResponseModel.Unauthorized);
            }
            await Task.CompletedTask;
        }
    }
}
