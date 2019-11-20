using System.ComponentModel.DataAnnotations;

namespace KWKY.Model.User
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
