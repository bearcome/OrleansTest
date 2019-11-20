using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitClientTest
{
    [CollectionDefinition(ClientCollection.Name)]
    public class ClientCollection : ICollectionFixture<ClientFixture>
    {
        public const string Name = "ClientCollection";
    }
}
