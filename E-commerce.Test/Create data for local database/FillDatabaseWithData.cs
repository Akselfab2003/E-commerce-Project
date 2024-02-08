using Bogus;
using Bogus.Extensions.UnitedStates;
using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace E_commerce.Test.Create_data_for_local_database
{
    [TestCaseOrderer("E_commerce.Test.OrderedTest", "E-commerce.Test")]

    [Collection("LocalDatabaseService")]
    public class FillDatabaseWithData
    {
        private readonly ITestOutputHelper output;
        private  IDataCollection DataCollection;

        public FillDatabaseWithData(GenerateFakeDataForDatabase collection,ITestOutputHelper outputHelper) 
        {
            this.output = outputHelper;
            this.DataCollection = collection.DataCollection;
        
        }

        [Fact,AttributePriority(-10)] 
        public async Task Insertusers()
        {
            Faker<Users> faker = new Faker<Users>()
                //.RuleFor(user => user.Id, data => data.IndexFaker)
                .RuleFor(user => user.Username, data => data.Person.UserName)
                .RuleFor(user => user.Password, data => "Test") /*data.Internet.Password(10)*/
                .RuleFor(user => user.Email, data => data.Person.Email)
                .RuleFor(user => user.Gender, data => data.Random.Bool());

            List<Users> data = faker.GenerateBetween(10, 20);

            output.WriteLine(JsonSerializer.Serialize(data));
            foreach (Users user in data)
            {
                user.Password = DataCollection.Cryptography.CreateNewPasswordHash(user.Password);

                await DataCollection.Users.Create(user);
            }
            Assert.True(data.Any());

        }

        [Fact, AttributePriority(-9)]
        public async void InsertDataForProducts()
        {
            string[] categories = new string[50];
            Faker<Categories> faker = new Faker<Categories>()
                .RuleFor(categories => categories.Name, data => data.Commerce.Categories(1)[0]) ;
 
            List<Categories> data = faker.GenerateBetween(50,50);

            foreach (Categories category in data.DistinctBy(t => t.Name).ToList())
            {
                await DataCollection.Categories.CreateCategories(category);
            }

            Assert.True(data.Any());
        }


        [Fact, AttributePriority(-7)]
        public async void GenerateFakeOrders()
        {
           
            Assert.True(await DataCollection.Users.GetById(1) != null, "No User was found!");

            List<Products> productlists = await DataCollection.Products.GetProducts(40);

            Assert.True(productlists.Count() > 0, "No products was found!");
            
            Users users = await DataCollection.Users.GetById(1);
            
            Assert.True(users != null,"No user was found!");

            Faker<Orders> faker = new Faker<Orders>()
                .RuleFor(orders => orders.OrderLines, data =>
                    new List<OrderDetails>()
                    {
                        new OrderDetails
                        {
                                Product  = productlists[data.Random.Number(0,productlists.Count()-1)],
                                price = Convert.ToDouble(data.Commerce.Price(0, 1000, 2, "")),
                                quantity =1,
                                total= Convert.ToDouble(data.Commerce.Price(0, 1000, 2, ""))
                        }
                    }
                    )
                .RuleFor(orders => orders.Users, data => users)
                .RuleFor(orders => orders.Session, data => new Session());
            List<Orders> fakeorders = faker.GenerateBetween(25, 40);
            foreach(Orders order in fakeorders)
            {
               await DataCollection.Orders.CreateOrder(order);
            }
        }



        [Fact(Skip = "GenerateFakeBaskets doesn't need to be runned any more")]
        public async void GenerateFakeBaskets()
        {
            
            List<Products> productlists = await DataCollection.Products.GetProducts(40);

            Assert.True(productlists.Count() > 0, "No products was found!");
            
            Assert.True(await DataCollection.Users.GetById(1) != null, "No User was found!");

            List<Session> sessions = await DataCollection.Session.GetAllSessions();

            Assert.True(sessions.Count() > 0, "No Session was found!");
    
            Session session = sessions.Where(ele => ele.user != null).First();
 
            Faker<Basket> faker = new Faker<Basket>()
                .RuleFor(basket => basket.BasketDetails, data =>
                    new List<BasketDetails>()
                    {
                        new BasketDetails
                        {
                                Products  = productlists[data.Random.Number(0,productlists.Count()-1)],
                            
                        }
                    }
                    )
                .RuleFor(orders => orders.Session, data => session);

            List<Basket> fakebasket = faker.GenerateBetween(1,1);
          
            await DataCollection.Basket.CreateBasket(fakebasket[0]);


        }



        [Fact, AttributePriority(-8)]

        public async Task InsertProducts()
        {
            List<Categories> categories = await DataCollection.Categories.GetAllUniqueCategories();
            Faker<Products> faker = new Faker<Products>()
                .RuleFor(Product => Product.Title, data => data.Commerce.ProductName())
                .RuleFor(Product => Product.Description, data => data.Commerce.ProductDescription())
                .RuleFor(Product => Product.Price, data => Convert.ToDouble(data.Commerce.Price(0, 1000, 2, "")))
                .RuleFor(Product => Product.Images, data => (new List<Images> { (new Images() { ImagePath = data.Image.PicsumUrl(1000, 1500, false, false) })}))
                .RuleFor(Product => Product.ProductCategories,data => data.PickRandom(categories));
            List<Products> data = faker.GenerateBetween(10, 20);

            
            foreach (Products product in data)
            {
                await DataCollection.Products.CreateProduct(product);
            }
            Assert.True(data.Any());

        }




        public async Task<List<PriceListEntity>> GetListPriceListEntities()
        {

            List<Products> products = await DataCollection.Products.GetProducts(40);

            Assert.True(products.Count() > 0);

            Faker<PriceListEntity> faker = new Faker<PriceListEntity>()
                .RuleFor(Entity => Entity.PriceListPrice, data => 1.0)
                .RuleFor(Entity => Entity.Product, data => products[data.IndexFaker]);



            List<PriceListEntity> ListOfPriceListEntities = faker.GenerateBetween(1, products.Count()-1);

            
            return ListOfPriceListEntities;


        }


        [Fact, AttributePriority(-5)]

        public async Task CreatePriceList()
        {
            Company company = await DataCollection.Company.GetById(1);
            Users user = await DataCollection.Users.GetById(2);

            Assert.NotNull(company);
            Assert.NotNull(user);



            Faker<PriceList> faker = new Faker<PriceList>()
                .RuleFor(pricelist => pricelist.Companies, data => new List<Company>() { company })
                .RuleFor(pricelist => pricelist.Users, data => new List<Users>() { user })
                .RuleFor(pricelist => pricelist.Name, data => "TestPriceList")
                .RuleFor(pricelist => pricelist.PriceListProducts, await GetListPriceListEntities());

            List<PriceList> priceLists = faker.GenerateBetween(1, 1);

            foreach (PriceList priceList in priceLists)
            {
                await DataCollection.PriceList.Create(priceList);
            }


        }


        [Fact, AttributePriority(-6)]
        public async Task CreateCompany()
        {
            Users users = await DataCollection.Users.GetById(1);
            Assert.NotNull(users);

            Faker<Company> faker = new Faker<Company>()
                .RuleFor(company => company.Name, data => data.Company.CompanyName())
                .RuleFor(company => company.email, data => data.Person.Email)
                .RuleFor(company => company.cvr, data => data.Company.Ein())
                .RuleFor(company => company.Users, data => new List<Users>() { users });


            List<Company> ListOfCompanies = faker.GenerateBetween(1, 1);

            foreach (Company company in ListOfCompanies)
            {
                await DataCollection.Company.Create(company);
            }



        }

    }
}
