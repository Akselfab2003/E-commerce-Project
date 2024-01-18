using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;
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
    public class BasketTests
    {
        private readonly IDataCollection dataCollection;
        private readonly ITestOutputHelper output;
        public BasketTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
        }


        [Fact]
        public async Task PostAndGetBasketByIdTest()
        {
            Basket basket = new Basket();
            basket.Session = new Session();
            basket.BasketDetails = new List<BasketDetails>();

            Basket basketReturned = await dataCollection.Basket.CreateBasket(basket);
            output.WriteLine(JsonSerializer.Serialize(basketReturned));
            Assert.Equal(basket, basketReturned);
        }
    }
}
