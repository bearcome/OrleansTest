using KWKY.Model.DBModel;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWKY.IGrains
{
    public interface IDemoTableGrain: IGrainWithGuidKey,IBaseGrain<DemoTable>
    {

        Task SetRandomData ();
    }
    public interface IDemoTableGrain1 : IGrainWithGuidCompoundKey
    { 
    
    }
}
