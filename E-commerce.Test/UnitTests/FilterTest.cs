using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace E_commerce.Test.UnitTests
{
    [Collection("Services")]

    public class FilterTest
    {
        private readonly IDataCollection dataCollection;

        private readonly ITestOutputHelper output;
        public FilterTest(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;

        }
        
        
        public async Task InsertTestData()
        {
           Categories categories = new Categories() { Id = 0, Name = "Test" };
           await dataCollection.Categories.CreateCategories(categories);

           Categories categories2 = new Categories() { Id = 0, Name = "Test2" };
           await dataCollection.Categories.CreateCategories(categories2);

            Categories categories3 = new Categories() { Id = 0, Name = "Test2" };
            await dataCollection.Categories.CreateCategories(categories3);



        }



        [Fact]
        public async Task Test_GetAllCategories()
        {
            await InsertTestData();

            List<Categories> categories = await dataCollection.Categories.GetAllUniqueCategories();
            try
            {
                foreach (var item in categories)
                {
                    output.WriteLine(JsonSerializer.Serialize(item));
                }

            }
            catch (Exception ex) 
            {

            }

            Assert.True(categories.Where(name => name.Name == "Test2").Count() == 1, "No categories was found");

            Assert.True(categories.Count() > 0, "No categories was found");
        }

    }
}
