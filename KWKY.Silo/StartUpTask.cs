using KWKY.IGrains;
using Orleans;
using Orleans.Runtime;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KWKY.Silo
{

    /// <summary>
    /// Silo启动后执行任务
    /// </summary>
    public class StartUpTask : IStartupTask
    {
        private readonly IGrainFactory _grainFactory;

        public StartUpTask (IGrainFactory grainFactory)
        {
            this._grainFactory = grainFactory;
        }

        public async Task Execute (CancellationToken cancellationToken)
        {
            var demoInstance = _grainFactory.GetGrain<IDemoTableGrain>(Guid.Parse("E3E425FE-68F5-4053-AF25-8FED5C275A71"));
            await demoInstance.WriteStateAsync();

            var demoInstance2 = _grainFactory.GetGrain<IDemoTableGrain>(Guid.Parse("E3E425FE-68F5-4053-AF25-8FED5C275A71"));

            var def = _grainFactory.GetGrain<IDefaultGrain>(Guid.NewGuid());
            var demo = _grainFactory.GetGrain<IJournaledDemoGrain>(Guid.NewGuid());
            await demo.WriteMethodDemo();
            await demo.WriteMethodDemo2();
            await Task.CompletedTask;

        }
    }

}
