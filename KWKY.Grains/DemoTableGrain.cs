using KWKY.DBUtility;
using KWKY.IGrains;
using KWKY.Model.DBModel;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWKY.Grains
{
    public class DemoTableGrain: BaseGrain<DemoTable>, IDemoTableGrain
    {
        public DemoTableGrain (KWKYDBContext dbcontext):base(dbcontext)
        { 
        
        }
        public async Task SetRandomData ()
        {
            State.Name = "hello";
            State.Age = 90;
            State.Birthday = DateTime.Now.AddYears(-90);
            await Task.CompletedTask;
        }


    }

    public class DemoTableGrain1 : IDemoTableGrain1
    {
        void Tesst ()
        {

            this.GetPrimaryKey();
            
        }
    }
}
