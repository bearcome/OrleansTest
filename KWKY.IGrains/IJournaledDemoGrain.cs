using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWKY.IGrains
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：IJournaledDemoGrain
	// 文件功能描述：
	//
	// 
	// 创建者：杨明
	// 时间：2019/8/12 9:23:16
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion

    public interface IJournaledDemoGrain: IGrainWithGuidKey
    {
        Task WriteMethodDemo ();
        Task WriteMethodDemo2 ();
    }
}
