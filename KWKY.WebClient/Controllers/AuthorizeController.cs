using KWKY.Common;
using KWKY.Model.User;
using KWKY.WebClient.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace KWKY.WebClient.Controllers
{

    [Route("api/[controller]", Name = "controllerName")]
    public class AuthorizeController : ControllerBase
    {
        private JwtSettings _jwtSettings;
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private IActionDescriptorCollectionProvider _provider;

        public AuthorizeController (JwtSettings jwtSettings, JwtSecurityTokenHandler jwtSecurityTokenHandler, IActionDescriptorCollectionProvider provider)
        {
            _jwtSettings = jwtSettings;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _provider = provider;
            GetRoutes();
        }
        private void GetRoutes ()
        {
            //var routes = _provider.ActionDescriptors.Items.Select(x => new {
            //    Controller = x.RouteValues["Controller"],
            //    Action = x.RouteValues["Action"],
            //    ControllerName = (((ControllerActionDescriptor)x).MethodInfo.DeclaringType.GetCustomAttributes(true).FirstOrDefault(o=>o is RouteAttribute) as RouteAttribute).Name,
            //    ActionName = x.AttributeRouteInfo.Name,
            //    HttpMethods = string.Join(ConstData.CommaChar,((HttpMethodActionConstraint)x.ActionConstraints.FirstOrDefault(o=>o is HttpMethodActionConstraint)).HttpMethods),
            //    x.AttributeRouteInfo.Template,
            //}).ToList();
            return;
        }



        [HttpPost("test", Name = "asdasdas")]
        [HttpPost("test2", Name = "asdasdas2")]
        public IActionResult Test (int id)
        {
            return new JsonResult("good");
        }
        /// <summary>
        /// asdasdasd
        /// </summary>
        /// <param name="viewModel">aaa</param>
        /// <param name="platformKey">bbb</param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Token ([FromBody]LoginModel viewModel, [FromHeader]string platformKey)
        {
            var routes = RouteData.Routers.OfType<RouteCollection>().ToList();
            if ( !( viewModel.UserName == "kwjq" && viewModel.Password == "Test123" ) )//判断账号密码是否正确
            {
                return BadRequest();
            }
#warning 未实现真是验证
            var claim = new Claim[]{
                new Claim(ConstData.ClaimTypes_UserId,Guid.NewGuid().ToString())
            };
            //对称秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtSecretKey));
            //签名证书(秘钥，加密算法)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                
            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
            var token = new JwtSecurityToken(_jwtSettings.JwtIssuer, _jwtSettings.JwtAudience, claim, DateTime.Now, DateTime.Now.AddMinutes(_jwtSettings.JwtExpiresMinute), creds);

            return Ok(new { token = _jwtSecurityTokenHandler.WriteToken(token) });

        }
    }
}