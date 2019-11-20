namespace KWKY.Model.Common
{
    /// <summary>
    /// 自定义状态吗
    /// </summary>
    public enum ResponseStateCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK = 200,
        /// <summary>
        /// 无权限
        /// </summary>
        Noauthorized = 201,
        /// <summary>
        /// 身份验证失败，无访问权限
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// 服务器执行失败
        /// </summary>
        ServerError = 500,
        /// <summary>
        /// 请求的资源未找到
        /// </summary>
        ResourceNotFound = 600,
        /// <summary>
        /// 参数无效
        /// </summary>
        ArgsInvalid = 601,
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordInvalid = 602,
        /// <summary>
        /// 数据已存在（用户名、手机号等）、资源已占用、资源受限
        /// </summary>
        ResourceAlreadyExist = 603,
        /// <summary>
        /// 对资源无操作权限
        /// </summary>
        AuthorLimited = 604
    }
}
