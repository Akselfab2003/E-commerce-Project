using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Test.Create_data_for_local_database
{
    [CollectionDefinition("LocalDatabaseService",DisableParallelization = true)]
    public class CreateLocalDataForDatabaseInjector : ICollectionFixture<GenerateFakeDataForDatabase>
    {
    }
}
