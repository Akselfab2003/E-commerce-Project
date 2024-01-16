using Bogus;
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

namespace E_commerce.Test.Create_data_for_local_database
{
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

        [Fact] 
        public async Task Insertusers()
        {
            Faker<Users> faker = new Faker<Users>()
                //.RuleFor(user => user.Id, data => data.IndexFaker)
                .RuleFor(user => user.Username, data => data.Person.UserName)
                .RuleFor(user => user.Password, data => data.Internet.Password(10))
                .RuleFor(user => user.Email, data => data.Person.Email)
                .RuleFor(user => user.Gender, data => data.Random.Bool());

            List<Users> data = faker.GenerateBetween(10, 20);

            output.WriteLine(JsonSerializer.Serialize(data));
            foreach (Users user in data)
            {
                await DataCollection.Users.CreateUser(user);
            }
            Assert.True(data.Any());

        }
        [Fact]
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


            await InsertProducts();



            Assert.True(data.Any());

        }


        [Fact]
        public async void GenerateFakeOrders()
        {
            await Insertusers();
            

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
                .RuleFor(orders => orders.Users, data => users);

            List<Orders> fakeorders = faker.GenerateBetween(25, 40);

            foreach(Orders order in fakeorders)
            {

               await DataCollection.Orders.CreateOrder(order);

            }


        }



        [Fact]
        public async void GenerateFakeBaskets()
        {
            await Insertusers();


            List<Products> productlists = await DataCollection.Products.GetProducts(40);
            Assert.True(productlists.Count() > 0, "No products was found!");
            Users users = await DataCollection.Users.GetById(1);
            List<Session> sessions = await DataCollection.Session.GetAllSessions();
            Session session = sessions.Where(ele => users.Id == 1 ).First();
           
            Assert.True(users != null, "No user was found!");

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

    }
}
