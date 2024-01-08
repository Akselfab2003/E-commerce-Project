using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;

namespace E_commerce.Test
{
    [Collection("Services")]

    public class UnitTest1
    {
        private readonly IDataCollection dataCollection;

        public UnitTest1(CreateFakeDBDependencies collection)
        {
            dataCollection = collection.DataCollection;
        }

        [Fact]
        public async void Test1()
        {
           Orders orders = new Orders();
           orders.Id = 1;
           orders.sessid = "Test";
           Orders orderReturned = await dataCollection.Orders.CreateOrder(orders);
           
           Assert.Equal(orders, orderReturned);
          
        }

        [Fact]
        public void Test2()
        {
            bool result = 1 == 1;

            Assert.True(result);
        }
    }
}