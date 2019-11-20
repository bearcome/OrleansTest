using Orleans.TestingHost;
using System;

namespace XUnitClusterTest
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public class ClusterFixture : IDisposable
#pragma warning restore CA1063 // Implement IDisposable Correctly
    {
        public ClusterFixture ()
        {
            this.Cluster = new TestClusterBuilder().Build();
            this.Cluster.Deploy();

        }

#pragma warning disable CA1063 // Implement IDisposable Correctly
        public void Dispose ()
#pragma warning restore CA1063 // Implement IDisposable Correctly
        {
            this.Cluster.StopAllSilos();
        }

        public TestCluster Cluster { get; private set; }
    }
}
