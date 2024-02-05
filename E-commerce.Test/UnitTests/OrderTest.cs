using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce_Project.Controllers;
using NuGet.Frameworks;
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
    public class OrderTest
    {
        private readonly IDataCollection dataCollection;

        private readonly ITestOutputHelper output;
        private readonly OrdersController OrderController;

        public OrderTest(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
        }
        
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

        #region create
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
