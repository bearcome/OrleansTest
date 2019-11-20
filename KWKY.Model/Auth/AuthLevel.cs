using KWKY.Common.Attributes;

namespace KWKY.Model.Auth
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：AuthLevel  
	// 文件功能描述：接口的默认最低权限级别，该数据不要入库，因为可能会变更导致与数据库不一致 
	//                  
	// 
	// 创建者：杨明
	// 时间：2019/5/7 9:24:36
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion
    public enum AuthLevel
    {
        /// <summary>
        /// 任何人都不可访问
        /// </summary>
        NULL,
        /// <summary>
        /// 维护单位
        /// </summary>
        Maintainer,
        /// <summary>
        /// 平台超级管理员
        /// </summary>
        PlatformSuperAdmin,
        /// <summary>
        /// 平台管理员
        /// </summary>
        PlatformAdmin,
        /// <summary>
        /// 特定的平台
        /// </summary>
        SpecificPlatform,
        /// <summary>
        /// 所有登录用户
        /// </summary>
        AllUsers,
        /// <summary>
        /// 匿名访客
        /// </summary>
        Anonymous
    }
    public enum AutoRole
    {
        /// <summary>
        /// 空角色
        /// </summary>
        [Summary("空角色")]
        NULL,
        /// <summary>
        /// 试用账号角色
        /// </summary>
        [Summary("试用角色")]
        Trial,
        /// <summary>
        /// 平台管理员
        /// </summary>
        [Summary("平台管理员")]
        PlatformAdmin,
        /// <summary>
        /// 平台超级管理员
        /// </summary>
        [Summary("平台超级管理员")]
        PlatformSuperAdmin,
        /// <summary>
        /// 维护单位
        /// </summary>
        [Summary("维护单位")]
        Maintainer,
    }
}
