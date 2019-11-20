namespace KWKY.Model.System
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：RouteInfo
	// 文件功能描述： 路由信息  
	//
	// 
	// 创建者：杨明
	// 时间：2019/5/9 9:48:02
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion
#warning 可设计为库表
    public class RouteInfo
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethods { get; set; }
        public string Template { get; set; }
    }
}
