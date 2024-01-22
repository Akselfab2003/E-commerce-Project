using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using System.Text.Json;
using Xunit.Abstractions;

namespace E_commerce.Test
{
    // [TestCaseOrderer("E_commerce.Test.OrderedTest", "E_commerce.Test")]
    [TestCaseOrderer("E_commerce.Test.OrderedTest", "E-commerce.Test")]
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

        [Fact, AttributePriority(2)]
        public async void Teststuffa()
        {
            var startTime = DateTime.Now;
            Orders orders = new Orders();
            orders.Id = 1;
            // orders.sessid = "Test";
            Orders orderReturned = dataCollection.Orders.CreateOrder(orders).Result;
            output.WriteLine(JsonSerializer.Serialize(orderReturned));
            Assert.Equal(orders, orderReturned);
            output.WriteLine($"DateTime: {startTime.ToString()} NanoSeconds: {startTime.Nanosecond}");

        }
        [Fact, AttributePriority(-11)]
        public void Teststuffb()
        {
            var startTime = DateTime.Now;

            Orders order = dataCollection.Orders.GetById(1).Result;

            output.WriteLine(JsonSerializer.Serialize(order));

            Assert.Null(order);
            output.WriteLine($"DateTime: {startTime.ToString()} NanoSeconds: {startTime.Nanosecond}");

        }

        [Fact, AttributePriority(-10)]
        public void Teststuffc()
        {
            DateTime startTime = DateTime.Now;

            dataCollection.Orders.DeleteOrder(1).Wait();
            Orders order = dataCollection.Orders.GetById(1).Result;
            output.WriteLine(JsonSerializer.Serialize(order));

            Assert.Null(order);
            output.WriteLine($"DateTime: {startTime.ToString()} NanoSeconds: {startTime.Nanosecond}");


        }




        //[Fact]
        //public async void Teststuff()
        //{
        //    var startTime = DateTime.Now;


        //    AdminUsers users = new AdminUsers();
        //    users.Username = "username";
        //    users.Password = "password";

        //    await dataCollection.AdminUsers.Create(users);


        //    AdminUsers user = await dataCollection.AdminUsers.GetByName("username");
        //    Assert.NotNull(user);
        //    var startTime = DateTime.Now;

        //    output.WriteLine(startTime.ToString());
        //}


    }
}