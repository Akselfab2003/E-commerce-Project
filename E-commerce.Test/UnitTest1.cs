using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using System.Text.Json;
using Xunit.Abstractions;

namespace E_commerce.Test
{
   // [TestCaseOrderer("E_commerce.Test.OrderedTest", "E_commerce.Test")]

    [Collection("Services")]
    public class UnitTest1
    {
        private readonly IDataCollection dataCollection;

        private readonly ITestOutputHelper output;
        public UnitTest1(CreateFakeDBDependencies collection,ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
        }

        [Fact]
        public async void TestA()
        {
        
                Orders orders = new Orders();
                orders.Id = 1;
                 orders.sessid = "Test";
                Orders orderReturned = await dataCollection.Orders.CreateOrder(orders);
                output.WriteLine(JsonSerializer.Serialize(orderReturned));
                Assert.Equal(orders, orderReturned);

        }
        [Fact]
        public async void TestD()
        {
            Orders order = await dataCollection.Orders.GetById(1);

            output.WriteLine(JsonSerializer.Serialize(order));

            Assert.Null(order);
        }

        [Fact]
        public async void TestB()
        {
        
            await dataCollection.Orders.DeleteOrder(1);
            Orders order = await dataCollection.Orders.GetById(1);
            output.WriteLine(JsonSerializer.Serialize(order));

            Assert.Null(order);


        }

    }
}