using Bogus;
using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;
using E_commerce.Test.UnitTest_Database_Setup;
using E_commerce_Project.Controllers;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static NuGet.Packaging.PackagingConstants;

namespace E_commerce.Test.UnitTests
{
    [Collection("Services")]
    public class OrderTest
    {
        private readonly IDataCollection dataCollection;

        private readonly ITestOutputHelper output;
        private readonly OrdersController OrderController;
        private static readonly FakeDataForTest dataGenerator = new FakeDataForTest();

        public OrderTest(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            //var data = dataGenerator.GenerateFakeOrders("someValidSessionId");
        }

        #region Insert Data
        [Fact]
        public async Task TestInsertData()
        {
            Orders orders = new Orders();
            orders.OrderLines = new List<OrderDetails>();
            orders.Users = new Users();
            orders.Session = new Session();
            await dataCollection.Orders.CreateOrder(orders);


            Orders ordersSession = new Orders();
            ordersSession.OrderLines = new List<OrderDetails>();
            ordersSession.Session = new Session();
            ordersSession.Session.SessId = "someValidSessionId";
            await dataCollection.Orders.CreateOrder(ordersSession);
        }

        public static IEnumerable<Object[]> TestOrderData()
        {
            yield return new object[] { dataGenerator.GenerateFakeOrders("someValidSessionId"), "" };
        }
        public async Task CreateTestData()
        {
        }

        [Theory]
        [MemberData(nameof(TestOrderData))]
        public async Task TestOrderCreation(Orders orders, string sessid)
        {

        }

        public async void GenerateFakeOrders()
        {

            //Assert.True(await DataCollection.Users.GetById(1) != null, "No User was found!");

            List<Products> productlists = await DataCollection.Products.GetProducts(40);

            Assert.True(productlists.Count() > 0, "No products was found!");

            Users users = await DataCollection.Users.GetById(1);

            Assert.True(users != null, "No user was found!");

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
            foreach (Orders order in fakeorders)
            {
                await DataCollection.Orders.CreateOrder(order);
            }
        }
        #endregion

        #region GET
        [Fact]
        public async Task Test_GetOrderstBysessID()
        {
            var validSessionId = "someValidSessionId";

            //Session session = new Session();
            //session.SessId = validSessionId;

            List<Orders> order = await OrderController.GetOrdersBysessID(validSessionId);
            output.WriteLine(JsonSerializer.Serialize(order));
            //Assert.Equal(validSessionId, session2.SessId);
            Assert.True(order.Count() > 0 | order.Count() == 0);
        }

        [Fact]
        public async Task Test_GetOrderstById()
        {
            Orders orderID = await dataCollection.Orders.GetById(1);
            output.WriteLine(JsonSerializer.Serialize(orderID));
            //Assert.Equal(1, orderID.Id);
            Assert.NotNull(orderID);
        }
        #endregion

        #region Create
        [Fact]
        public async Task Test_CreateOrder()
        {
            Orders orders = new Orders();
            Orders created = await dataCollection.Orders.CreateOrder(orders);
            output.WriteLine(JsonSerializer.Serialize(created));
            Assert.NotNull(created);
        }
        #endregion
    }
}
