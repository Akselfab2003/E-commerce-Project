using Bogus;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;
using E_commerce_Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_commerce.Test.UnitTests
{
    [Collection("Services")]

    public class ProductTests 
    {
        private readonly IDataCollection dataCollection;
        private readonly ITestOutputHelper output;
        private ProductsController productsController;
        public ProductTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            GenerateFakeProductData();
            productsController = new ProductsController(dataCollection);
        }


        [Fact]
        public async  void Can_Create_Update_Delete_product()
        {
           #region CREATE Product
               Products product = new Products();
               product.Title = "Create";
               product.Price = 1.1;
               product.Description = "Description";

               Products ProductCreated = await CreateProduct(product);

           #endregion

           #region UPDATE Product
               ProductCreated.Title = "Update";

               Products ProductUpdated = await UpdateProduct(ProductCreated);

               Assert.True(ProductUpdated.Title == "Update", "Update Product Failed");
           #endregion

           #region DELETE Product

               bool DeleteProjectResult = await DeleteProduct(product);
               Assert.True(DeleteProjectResult == true, "Delete Failed");

           #endregion
        }


        #region CRUD Methods
        public async Task<Products> CreateProduct(Products product)
        {
            Products ProductCreated = await dataCollection.Products.Create(product);

            Assert.True(ProductCreated != null, "Create Product Failed");

            output.WriteLine($"Created: {JsonSerializer.Serialize(ProductCreated)}");

            return ProductCreated;
        }

        public async Task<Products> UpdateProduct(Products product)
        {
            Products ProductUpdated = await dataCollection.Products.Update(product);

            output.WriteLine($"Update: {JsonSerializer.Serialize(ProductUpdated)}");

            Assert.True(ProductUpdated != null, "Update Product Failed");

            return ProductUpdated;
        }
        public async Task<bool> DeleteProduct(Products product)
        {
            await dataCollection.Products.Delete(product);

            Products ShouldBeNullIfDeletedSucced = await dataCollection.Products.GetById(product.Id);

            output.WriteLine($"Select For Deleted Product: {JsonSerializer.Serialize(ShouldBeNullIfDeletedSucced)}");

            return (ShouldBeNullIfDeletedSucced == null ? true : false);
        }

        #endregion

        #region Testdata For Methods
        public static IEnumerable<object[]> IdTestDataForGetById()
        {
            yield return new object[] { 1 };
            yield return new object[] { 2 };
            yield return new object[] { 3 };
            yield return new object[] { 4 };

        }

        public static IEnumerable<object[]> CountTestDataForGetProducts()
        {
            yield return new object[] { 5 };
            yield return new object[] { 10 };
            yield return new object[] { 20 };
            yield return new object[] { 4 };

        }

        public static IEnumerable<object[]> SearchTestDataForSearchForProducts()
        {
            yield return new object[] { "e1" };
            yield return new object[] { "e2" };
            yield return new object[] { "aaaaaaaaaaaaaaaaaaaaaaaa" };
            yield return new object[] { "e4" };

        }
        private void GenerateFakeProductData()
        {
            Faker<Products> faker = new Faker<Products>()
              .RuleFor(Product => Product.Title, data => ("ProductName"+data.IndexGlobal.ToString()))
              .RuleFor(Product => Product.Description, data => data.Commerce.ProductDescription())
              .RuleFor(Product => Product.Price, data => Convert.ToDouble(data.Commerce.Price(0, 1000, 2, "")));
            List<Products> data = faker.GenerateBetween(20, 20);


            foreach (Products product in data)
            {
                 dataCollection.Products.CreateProduct(product).Wait();
            }

        }

        #endregion

        [Fact]
        public async void Test_GetAllProducts()
        {
           

           List<Products>? GetAllproducts =await productsController.GetAllProducts();

           Assert.True(GetAllproducts != null, "GetAllProducts Returned null");

           Assert.True(GetAllproducts.Count > 0, "GetAllProducts didn't Returned any Products");


        }
        [Theory]
        [MemberData(nameof(IdTestDataForGetById))]
        public async void Test_GetById(int id)
        {
           Products Result = await dataCollection.Products.GetById(id);
           Assert.NotNull(Result);
        }

        [Theory]
        [MemberData(nameof(CountTestDataForGetProducts))]
        public async void Test_GetProducts(int Count)
        {
            List<Products> Result = await dataCollection.Products.GetProducts(Count);

            Assert.True(Result.Count == Count);
        }

        [Theory]
        [MemberData(nameof(SearchTestDataForSearchForProducts))]
        public async void Test_SearchForProducts(string SearchInput)
        {
            if (SearchInput == "aaaaaaaaaaaaaaaaaaaaaaaa")
            {

                List<Products> products = await dataCollection.Products.SearchForProducts(SearchInput);

                output.WriteLine($"Created: {JsonSerializer.Serialize(products.Count())}");

                Assert.True(products.Count() == 0);

            }
            else
            {
                List<Products> products = await dataCollection.Products.SearchForProducts(SearchInput);
            }
                  
        }

    }
}
