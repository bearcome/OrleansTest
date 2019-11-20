using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KWKY.WebClient.Controllers
{
    /// <summary>
    /// 通用数据格式验证  需要ActionFilter去掉全局模型验证  改为Attribute
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommonVerifyController : ControllerBase
    {
        //[HttpPost("email")]
        //public IActionResult VerifyEmail ([System.ComponentModel.DataAnnotations.EmailAddress]string email)
        //{
        //    if ( !_userRepository.VerifyEmail(email) )
        //    {
        //        return Json($"Email {email} is already in use.");
        //    }

        //    return Json(true);
        //}
        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyPhone ([RegularExpression(@"^\d{3}-\d{3}-\d{4}$")] string phone)
        {
            if ( !ModelState.IsValid )
            {
                return new JsonResult($"Phone {phone} has an invalid format. Format: ###-###-####");
            }

            return new JsonResult(true);
        }
    }
}