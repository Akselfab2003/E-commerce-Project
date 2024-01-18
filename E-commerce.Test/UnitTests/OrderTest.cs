using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
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
        public OrderTest(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
        }

        [Fact]
        public async Task Test_GetOrderstBysessID()
        {
            var validSessionId = "someValidSessionId";
            var invalidSessionId = "someInvalidSessionId";
            invalidSessionId = "someInValidSessionId";


            Session session = new Session();
            session.SessId = validSessionId;

            Session session2 = await dataCollection.Session.CreateSession(session);
            output.WriteLine(JsonSerializer.Serialize(session2));
            Assert.Equal(validSessionId, session2.SessId);
        }
        public async Task TestInsertData()
        {
            Orders orders = new Orders();
            orders.OrderLines = new List<OrderDetails>();
            orders.Users = new Users();
            await dataCollection.Orders.CreateOrder(orders);
        }

        [Fact]
        public async Task Test_GetOrderstById()
        {
            await TestInsertData();
            Orders orderID = await dataCollection.Orders.GetById(1);
            output.WriteLine(JsonSerializer.Serialize(orderID));
            //Assert.Equal(1, orderID.Id);
            Assert.NotNull(orderID);
        }

        

    }
}
