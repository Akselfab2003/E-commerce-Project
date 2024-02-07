using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce_Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly FilterController filterController;

        public FilterTest(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            filterController = new FilterController(dataCollection);


        }



        [Fact]
        public async void Can_Create_Update_Delete_Category()
        {
            #region CREATE Category
            Categories Category = new Categories();
            Category.Name= "Create";
          


            await CreateCategory(Category);

            #endregion

            #region UPDATE Category
            Category.Name = "Update";

            await UpdateCategory(Category);

            Assert.True(Category.Name == "Update", "Update Category Failed");
            #endregion

            #region DELETE Category

            bool DeleteProjectResult = await DeleteCategory(Category);
            Assert.True(DeleteProjectResult == true, "Delete Failed");

            #endregion
        }


        #region CRUD Methods
        public async Task CreateCategory(Categories Category)
        {
            HttpStatusCode Result = await filterController.CreateCategories(Category);
            output.WriteLine(JsonSerializer.Serialize(Category));
            output.WriteLine($"Created: {Result}");
            Assert.True(Result == HttpStatusCode.Created, "Create Category Failed");


        }

        public async Task UpdateCategory(Categories Category)
        {

            HttpStatusCode Result = await filterController.UpdateCategories(Category.Id, Category);
            output.WriteLine($"Update: {Result}");

            Assert.True(Result == HttpStatusCode.OK, "Update Category Failed");


        }
        public async Task<bool> DeleteCategory(Categories Category)
        {
            await filterController.DeleteCategories(Category.Id);

            Categories ShouldBeNullIfDeletedSucced = await filterController.GetCategoryById(Category.Id);

            output.WriteLine($"Select For Deleted Category: {JsonSerializer.Serialize(ShouldBeNullIfDeletedSucced)}");

            return (ShouldBeNullIfDeletedSucced == null ? true : false);
        }

        #endregion





        public async Task InsertTestData()
        {
            Categories categories = new Categories() { Id = 0, Name = "Test" };
            await filterController.CreateCategories(categories);

            Categories categories2 = new Categories() { Id = 0, Name = "Test2" };
            await filterController.CreateCategories(categories2);

            Categories categories3 = new Categories() { Id = 0, Name = "Test2" };
            await filterController.CreateCategories(categories3);



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
