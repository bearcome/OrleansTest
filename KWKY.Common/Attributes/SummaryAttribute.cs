using System;

namespace KWKY.Common.Attributes
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：SummaryAttribute
	// 文件功能描述： 用于描述字段，获取字段描述，避免代码中出现大量字符串
	//
	// 
	// 创建者：杨明
	// 时间：2019/5/7 10:56:44
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Enum)]
    public class SummaryAttribute : Attribute
    {
        public SummaryAttribute (string description)
        {
            Description = description;
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Description { get; set; }
    }
}
