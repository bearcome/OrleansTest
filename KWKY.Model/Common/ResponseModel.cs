namespace KWKY.Model.Common
{
    /// <summary>
    /// Api 返回数据模型
    /// </summary>
    public class ResponseModel
    {
        public ResponseModel ()
        { 
        
        }
        /// <summary>
        /// 状态码
        /// </summary>
        public ResponseStateCode StateCode { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 文字信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 重定向或跳转URL
        /// </summary>
        public string RedirectUrl { get; set; }


        #region 常用返回结构,避免频繁创建销毁  常用的可以继续添加
        public static readonly ResponseModel ArgsInvalid = new ResponseModel() { StateCode = ResponseStateCode.ArgsInvalid, Message = "参数无效" };
        public static readonly ResponseModel BadRequest = new ResponseModel() { StateCode = ResponseStateCode.ServerError, Message = "请求失败，请重试" };
        public static readonly ResponseModel Ok = new ResponseModel() { StateCode = ResponseStateCode.OK, Message = "操作成功" };
        public static readonly ResponseModel Unauthorized = new ResponseModel() { StateCode = ResponseStateCode.Unauthorized, Message = "您没有操作权限" };
        #endregion

    }
}
