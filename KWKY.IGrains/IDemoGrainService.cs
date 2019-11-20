using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWKY.IGrains
{
    public interface IDemoGrainService:IGrainService
    {
        Task DemoService ();
    }
}
