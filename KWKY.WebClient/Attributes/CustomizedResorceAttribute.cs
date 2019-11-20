using System;

namespace KWKY.WebClient.Attributes
{
    /// <summary>
    /// 标识资源信息  用于定制化接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomizedResorceAttribute : Attribute
    {
        public CustomizedResorceAttribute (string platformName)
        {
            PlatformName = platformName;
        }
        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName { get; set; }

    }
}
