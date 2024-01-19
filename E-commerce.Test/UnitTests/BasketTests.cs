using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
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
        public async Task PostAndGetBasketByIdsTest()
        {
            Basket basket = new Basket();
            basket.Session = new Session();
            basket.BasketDetails = new List<BasketDetails>();

            Basket basketEqual = await dataCollection.Basket.CreateBasket(basket);
            output.WriteLine(JsonSerializer.Serialize(basketEqual));
            Assert.Equal(basket, basketEqual);

            Basket basketReturnedUsingId = await dataCollection.Basket.GetById(basket.Id);
            output.WriteLine(JsonSerializer.Serialize(basketReturnedUsingId));
            Assert.Equal(basket, basketReturnedUsingId);

            Basket basketReturnedUsingSessId = await dataCollection.Basket.GetBySessId(basket.Session);
            output.WriteLine(JsonSerializer.Serialize(basketReturnedUsingSessId));
            Assert.Equal(basket, basketReturnedUsingSessId);

            basket.Session.SessId = "Updated-SessId";
            //BasketDetails basketDetails = new BasketDetails();
            //basket.BasketDetails.Add(basketDetails);
            Basket updatedBasket = await dataCollection.Basket.UpdateBasket(basket);
            Assert.True(updatedBasket.Session.SessId == basket.Session.SessId);
            output.WriteLine(JsonSerializer.Serialize(updatedBasket));

            bool deletedBasket = await dataCollection.Basket.DeleteBasket(basket);
            Assert.True(deletedBasket);
            output.WriteLine(JsonSerializer.Serialize(deletedBasket));
        }
    }
}
