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
            OrderController = new OrdersController(dataCollection);
            GeneradeTestOrders();
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
        #endregion

        public async void GeneradeTestOrders()
        {
            Orders data = (await dataGenerator.GenerateFakeOrders("someValidSessionId"))[0];
            Session session = new Session();
            session.SessId = "someValidSessionId";

            await dataCollection.Session.Create(session);
            data.Session = session;
            await dataCollection.Orders.Create(data);
        }

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
            Assert.True(order.Count() > 0);
        }

        [Fact]
        public async Task Test_GetOrderstById()
        {
            Orders orderID = await OrderController.GetOrdersById(1);
            output.WriteLine(JsonSerializer.Serialize(orderID));
            //Assert.Equal(1, orderID.Id);
            Assert.NotNull(orderID);
        }
        #endregion

        #region Create
        [Fact]
        public async void Test_CreateOrder()
        {
            Orders orders = (await dataGenerator.GenerateFakeOrders("someValidSessionId"))[1];
            Session newSession = await OrderController.CreateOrder("someValidSessionId", orders);
            output.WriteLine(JsonSerializer.Serialize(newSession));
            Assert.NotNull(newSession);
        }
        #endregion
    }
}
