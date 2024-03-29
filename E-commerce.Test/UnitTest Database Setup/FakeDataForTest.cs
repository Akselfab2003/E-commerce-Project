﻿using Bogus;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Bogus.Extensions.UnitedStates;

namespace E_commerce.Test.UnitTest_Database_Setup
{
    public class FakeDataForTest
    {
        private IDataCollection DataCollection;

        public FakeDataForTest(IDataCollection collection)
        {
            this.DataCollection = collection;

        }

        public FakeDataForTest()
        {

        }

        public async Task<Users> GetFakeusers()
        {
            Faker<Users> faker = new Faker<Users>()
                //.RuleFor(user => user.Id, data => data.IndexFaker)
                .RuleFor(user => user.Username, data => data.Person.UserName)
                .RuleFor(user => user.Password, data => "Test") /*data.Internet.Password(10)*/
                .RuleFor(user => user.Email, data => data.Person.Email)
                .RuleFor(user => user.Gender, data => data.Random.Bool());

            return faker.Generate();



        }
        public async Task<Reviews> GetReviews()
        {
            List<Products> productlists = await InsertProducts();

            Faker<Reviews> faker = new Faker<Reviews>()
                //.RuleFor(user => user.Id, data => data.IndexFaker)
                .RuleFor(user => user.Id, data => 0)
                .RuleFor(user => user.Products, data => (productlists[0])) /*data.Internet.Password(10)*/
                .RuleFor(user => user.ReviewContent, data => data.Rant.Review())
                .RuleFor(user => user.ReviewTitle, data => data.Rant.Review())
                .RuleFor(user => user.ReviewRating, data => data.Random.Int(0, 5));

            return faker.Generate();



        }
        public async Task<List<Categories>> InsertDataForProducts()
        {
            string[] categories = new string[50];
            Faker<Categories> faker = new Faker<Categories>()
                .RuleFor(categories => categories.Name, data => data.Commerce.Categories(1)[0]);

            List<Categories> data = faker.GenerateBetween(50, 50);

            return data.DistinctBy(t => t.Name).ToList();

        }


        public async Task<List<Orders>> GenerateFakeOrders(string sessid)
        {

            //Assert.True(await DataCollection.Users.GetById(1) != null, "No User was found!");

            List<Products> productlists = await InsertProducts(); // await DataCollection.Products.GetProducts(40);

            //Assert.True(productlists.Count() > 0, "No products was found!");

            //Users users = await DataCollection.Users.GetById(1);

            //Assert.True(users != null, "No user was found!");

            Faker<Orders> faker = new Faker<Orders>()
                .RuleFor(orders => orders.OrderLines, data =>
                    new List<OrderDetails>()
                    {
                        new OrderDetails
                        {
                                Product  = productlists[data.Random.Number(0,productlists.Count()-1)],
                                price =  productlists[data.Random.Number(0,productlists.Count()-1)].Price,
                                quantity =2,
                                total= productlists[data.Random.Number(0,productlists.Count()-1)].Price * 2,
                        }
                    }
                    );
            List<Orders> fakeorders = faker.GenerateBetween(25, 40);
            
            return fakeorders;
           
        }



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

            List<Basket> fakebasket = faker.GenerateBetween(1, 1);

            await DataCollection.Basket.CreateBasket(fakebasket[0]);


        }




        public async Task<List<Products>> InsertProducts()
        {

            List<Categories> categories = await InsertDataForProducts();
            Faker<Products> faker = new Faker<Products>()
                .RuleFor(Product => Product.Title, data => data.Commerce.ProductName())
                .RuleFor(Product => Product.Description, data => data.Commerce.ProductDescription())
                .RuleFor(Product => Product.Price, data => Convert.ToDouble(data.Commerce.Price(0, 1000, 2, "")))
                .RuleFor(Product => Product.Images, data => (new List<Images> { (new Images() { ImagePath = data.Image.PicsumUrl(1000, 1500, false, false) }) }))
                .RuleFor(Product => Product.ProductCategories, data => data.PickRandom(categories));
            List<Products> data = faker.GenerateBetween(10, 20);

            return data;

        }




        public async Task<List<PriceListEntity>> GetListPriceListEntities(List<Products> products)
        {

        

            Assert.True(products.Count() > 0);

            Faker<PriceListEntity> faker = new Faker<PriceListEntity>()
                .RuleFor(Entity => Entity.PriceListPrice, data => 1.0)
                .RuleFor(Entity => Entity.Product, data => products[data.IndexFaker]);



            List<PriceListEntity> ListOfPriceListEntities = faker.GenerateBetween(1, products.Count() - 1);


            return ListOfPriceListEntities;


        }



        public async Task<PriceList> CreatePriceList(Company cmp,Users usr,List<Products> products)
        {
            Company company = cmp;// await DataCollection.Company.GetById(1);
            Users user = usr;//await DataCollection.Users.GetById(2);

            Assert.NotNull(company);
            Assert.NotNull(user);



            Faker<PriceList> faker = new Faker<PriceList>()
                .RuleFor(pricelist => pricelist.Companies, data => new List<Company>() { company })
                .RuleFor(pricelist => pricelist.Users, data => new List<Users>() { user })
               
                .RuleFor(pricelist => pricelist.Name, data => data.Company.CompanyName())
                .RuleFor(pricelist => pricelist.PriceListProducts, await GetListPriceListEntities(products));

            List<PriceList> priceLists = faker.GenerateBetween(1, 1);


            return priceLists[0];




        }


        public async Task<List<Company>> CreateCompany()
        {
            Faker<Company> faker = new Faker<Company>()
                .RuleFor(company => company.Name, data => data.Company.CompanyName())
                .RuleFor(company => company.email, data => data.Person.Email)
                .RuleFor(company => company.cvr, data => data.Company.CompanySuffix())
                .RuleFor(company => company.Users, data => new List<Users>() {  });


            List<Company> ListOfCompanies = faker.GenerateBetween(1, 1);
            return ListOfCompanies;



        }

    

}
}
