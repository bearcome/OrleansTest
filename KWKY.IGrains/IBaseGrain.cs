using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWKY.IGrains
{
    public interface IBaseGrain<T>: IGrainWithGuidKey where T : class, new()
    {
        Task<int> WriteStateAsync ();
        Task<int> DeleteStateAsync ();

    }
}
