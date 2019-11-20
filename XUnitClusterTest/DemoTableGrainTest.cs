using KWKY.IGrains;
using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitClusterTest
{
    [Collection(ClusterCollection.Name)]
    public class DemoTableGrainTest
    {
        private readonly TestCluster _cluster;

        public DemoTableGrainTest (ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
        }

        [Fact]
        public async Task SaysHelloCorrectly ()
        {
            var demo = _cluster.GrainFactory.GetGrain<IDemoTableGrain>(Guid.NewGuid());
            demo.SetRandomData();
            int res = await demo.WriteStateAsync();

            Assert.Equal(1,res);
        }
    }
}
