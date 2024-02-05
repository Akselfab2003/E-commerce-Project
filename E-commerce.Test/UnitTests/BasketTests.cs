using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;
using E_commerce_Project.Controllers;
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
        private BasketController basketController;
        public BasketTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
        }

        #region GetTests
        [Fact]
        public async Task GetBasketById()
        {
            Basket basket = new Basket();
            basket.Session = new Session();
            basket.BasketDetails = new List<BasketDetails>();

            Basket basketEqual = await dataCollection.Basket.CreateBasket(basket);

            Basket basketReturnedUsingId = await dataCollection.Basket.GetById(basket.Id);
            output.WriteLine(JsonSerializer.Serialize(basketReturnedUsingId));
            Assert.Equal(basket, basketReturnedUsingId);


        }


        [Fact]
        public async Task GetBasketBySessId()
        {
            Basket basket = new Basket();
            basket.Session = new Session();
            basket.BasketDetails = new List<BasketDetails>();

            Basket basketEqual = await dataCollection.Basket.CreateBasket(basket);

            Basket basketReturnedUsingId = await dataCollection.Basket.GetById(basket.Id);
            output.WriteLine(JsonSerializer.Serialize(basketReturnedUsingId));
            Assert.Equal(basket, basketReturnedUsingId);
        }
        #endregion

        #region PostTests
        [Fact]
        public async Task PostBasket()
        {
            Basket basket = new Basket();
            basket.Session = new Session();
            basket.BasketDetails = new List<BasketDetails>();

            Basket basketEqual = await dataCollection.Basket.CreateBasket(basket);
            output.WriteLine(JsonSerializer.Serialize(basketEqual));
            Assert.Equal(basket, basketEqual);
        }
        #endregion

        #region PutTests
        [Fact]
        public async Task PutBasket()
        {
            Basket basket = new Basket();
            basket.Session = new Session();
            basket.BasketDetails = new List<BasketDetails>();

            basket.Session.SessId = "Updated-SessId";
            Basket updatedBasket = await dataCollection.Basket.UpdateBasket(basket);
            Assert.True(updatedBasket.Session.SessId == basket.Session.SessId);
            output.WriteLine(JsonSerializer.Serialize(updatedBasket));
        }
        #endregion

        #region DeleteTests
        [Fact]
        public async Task DeleteBasket()
        {
            Basket basket = new Basket();
            basket.Session = new Session();
            basket.BasketDetails = new List<BasketDetails>();

            Basket basketEqual = await dataCollection.Basket.CreateBasket(basket);

            bool deletedBasket = await dataCollection.Basket.DeleteBasket(basket);
            Assert.True(deletedBasket);
            output.WriteLine(JsonSerializer.Serialize(deletedBasket));
        }
        #endregion
    }
}
