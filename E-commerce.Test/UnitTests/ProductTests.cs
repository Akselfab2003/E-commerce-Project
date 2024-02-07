using Bogus;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;
using E_commerce.Test.Create_data_for_local_database;
using E_commerce.Test.UnitTest_Database_Setup;
using E_commerce_Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private FillDatabaseWithData fillDatabaseWithData;
        private readonly FakeDataForTest fakeDataForTest;
        public ProductTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            Insertsessions().Wait();    
            productsController = new ProductsController(dataCollection);
            fakeDataForTest  = new FakeDataForTest();
            GenerateFakeProductData().Wait();
        }


        [Fact]
        public async  void Can_Create_Update_Delete_product()
        {
           #region CREATE Product
               Products product = new Products();
               product.Title = "Create";
               product.Price = 1.1;
               product.Description = "Description";
               product.Images = new List<Images>();
               

               await CreateProduct(product);

           #endregion

           #region UPDATE Product
               product.Title = "Update";

               await UpdateProduct(product);

               Assert.True(product.Title == "Update", "Update Product Failed");
           #endregion

           #region DELETE Product

               bool DeleteProjectResult = await DeleteProduct(product);
               Assert.True(DeleteProjectResult == true, "Delete Failed");

           #endregion
        }


        #region CRUD Methods
        public async Task CreateProduct(Products product)
        {
            HttpStatusCode Result = await productsController.PostProduct(product);
            output.WriteLine(JsonSerializer.Serialize(product));
            output.WriteLine($"Created: {Result}");
            Assert.True(Result == HttpStatusCode.Created, "Create Product Failed");


        }

        public async Task UpdateProduct(Products product)
        {

            HttpStatusCode Result = await productsController.PutProduct(product.Id,product);
            output.WriteLine($"Update: {Result}");

            Assert.True(Result == HttpStatusCode.OK, "Update Product Failed");

           
        }
        public async Task<bool> DeleteProduct(Products product)
        {
            await productsController.DeleteProduct(product.Id);

            Products ShouldBeNullIfDeletedSucced = await productsController.GetProductById(product.Id);

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


        public async Task Insertsessions()
        {

          

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
            yield return new object[] { "e1", "Test_User_alone_pricelist" };
            yield return new object[] { "e2", "Company_User" };
            yield return new object[] { "aaaaaaaaaaaaaaaaaaaaaaaa", "" };

            yield return new object[] { "e","" };
          


        }
        private async Task GenerateFakeProductData()
        {
            
            Faker<Products> faker = new Faker<Products>()
              .RuleFor(Product => Product.Title, data => ("ProductName"+data.IndexGlobal.ToString()))
              .RuleFor(Product => Product.Description, data => data.Commerce.ProductDescription())
              .RuleFor(Product => Product.Price, data => Convert.ToDouble(data.Commerce.Price(2, 1000, 2, "")));
            List<Products> data = faker.GenerateBetween(20, 20);


            foreach (Products product in data)
            {
                await productsController.PostProduct(product);
            }
           // await fillDatabaseWithData.CreateCompany();
           // await fillDatabaseWithData.Insertusers();
           Company company = new Company();
           company.Name = "Test";
            //company.Users = new List<Users>();
            await dataCollection.Company.Create(company);

           Users usr = new Users();
           usr.Username = "Test User alone pricelist";
            await dataCollection.Users.Create(usr);

            Users usrTestCompany = new Users();
            usrTestCompany.Username = "Company_User";
            await dataCollection.Users.Create(usrTestCompany);

            //company.Users.Add(usr);
            Session session = new Session();
            session.SessId = "Test_User_alone_pricelist";
            session.user = usr;

            await dataCollection.Session.Create(session);



            Session sessionForCompany = new Session();
            sessionForCompany.SessId = "Company_User";
            sessionForCompany.user = usr;

            await dataCollection.Session.Create(sessionForCompany);

            PriceList priceList = await fakeDataForTest.CreatePriceList(company, usr, data);
            
            await dataCollection.PriceList.Create(priceList);


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
           Products Result = await productsController.GetProductById(id);
           output.WriteLine(JsonSerializer.Serialize(Result));
           Assert.NotNull(Result);
        }

        [Theory]
        [MemberData(nameof(CountTestDataForGetProducts))]
        public async void Test_GetProducts(int Count)
        {
            List<Products> Result = await productsController.GetLimitedAmountOfProducts(Count);
            output.WriteLine($"{Result.Count()}");
            Assert.True(Result.Count == Count);
        }

        [Theory]
        [MemberData(nameof(SearchTestDataForSearchForProducts))]
        public async void Test_SearchForProducts(string SearchInput,string sessid)
        {
            if(sessid != "")
            {
                List<Products> products = await productsController.SearchForProducts(SearchInput, sessid);
                output.WriteLine($"Created: {JsonSerializer.Serialize(products)}");

                Assert.True(products.Any(ele => ele.Price == 1));
            }


            if (SearchInput == "aaaaaaaaaaaaaaaaaaaaaaaa")
            {

                List<Products> products = await productsController.SearchForProducts(SearchInput, sessid);

                output.WriteLine($"Created: {JsonSerializer.Serialize(products.Count())}");

                Assert.True(products.Count() == 0);

            }
            else
            {
                List<Products> products = await productsController.SearchForProducts(SearchInput, sessid);
                Assert.True(products.Count() > 0);
                Assert.True(products.All(ele => ele.Price != 1));

            }

        }

    }
}
