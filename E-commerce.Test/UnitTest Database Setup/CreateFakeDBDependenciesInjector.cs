using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Test
{
    [CollectionDefinition("Services")]

    public class CreateFakeDBDependenciesInjector : ICollectionFixture<CreateFakeDBDependencies>
    {
    }
}
