using System;
using System.Collections.Generic;
using System.Text;

namespace KWKY.Model.KWKYFormModel
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：JournaledState
	// 文件功能描述：
	//
	// 
	// 创建者：杨明
	// 时间：2019/8/12 9:26:38
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion

    [Serializable]
    public partial class Crf
    {
        public void Apply (EventDemo1 demo1)
        {
            //Id = demo1.Id;
            //Name = demo1.Name;
            //Status = demo1.Age;
        }
    }

    [Serializable]
    public class EventDemo1
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public sbyte Age { get; set; }
    }
}
