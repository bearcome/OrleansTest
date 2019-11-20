namespace KWKY.WebClient.Authentication
{
    public class JwtSettings
    {
        /// <summary>
        /// token颁发者
        /// </summary>
        public string JwtIssuer { get; set; }
        /// <summary>
        /// token可以给那些客户端使用
        /// </summary>
        public string JwtAudience { get; set; }
        /// <summary>
        /// 加密key
        /// </summary>
        public string JwtSecretKey { get; set; }
        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int JwtExpiresMinute { get; set; } = 30;
    }
}
