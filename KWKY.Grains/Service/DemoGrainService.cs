using KWKY.IGrains;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWKY.Grains.Service
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：DemoGrainService
	// 文件功能描述：
	//
	// 
	// 创建者：杨明
	// 时间：2019/8/12 15:23:27
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion

    public class DemoGrainService : GrainService, IDemoGrainService
    {
        public async Task DemoService ()
        {
            await Task.CompletedTask;
        }
    }
}
