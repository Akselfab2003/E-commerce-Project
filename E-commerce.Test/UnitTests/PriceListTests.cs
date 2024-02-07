using Bogus.DataSets;
using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using E_commerce_Project.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Collections.Specialized.BitVector32;

namespace E_commerce.Test.UnitTests
{
    [Collection("Services")]
    public class PriceListTests
    {

        private readonly IDataCollection dataCollection;

        private PriceListController priceListController;

        private readonly ITestOutputHelper output;

        public PriceListTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            priceListController = new PriceListController(dataCollection);
            output = outputHelper;
            InsertTestData();
        }
        #region DataParameters
        public static IEnumerable<Object[]> PriceListIds()
        {
            yield return new object[] { "1" };
        }
        public static IEnumerable<Object[]> PriceListIdsForDelete()
        {
            yield return new object[] { "99" };
        }
        public static IEnumerable<Object[]> PriceListUpdateId()
        {
            yield return new object[] { "1" };
            yield return new object[] { "3" };

        }
        #endregion

        #region InsertData
        public void InsertTestData()
        {
            for (int i = 1; i <= 3; i++)
            {
                PriceList priceList = new PriceList() {Id=i, Name = $"PriceListName{i}",Companies=null,Users=null,PriceListProducts=null};
                dataCollection.PriceList.Create(priceList).Wait();
            }
            PriceList priceListDelete = new PriceList() { Id = 99, Name = $"PriceListName99", Companies = null, Users = null, PriceListProducts = null };
            dataCollection.PriceList.Create(priceListDelete).Wait();
        }
        #endregion

        #region POST requests test
        [Fact, AttributePriority(2)]
        public async Task CreatePriceList()
        {
            PriceList priceList = new PriceList();
            try
            {
                priceList.Name = "Lars";
                priceList.Users = new List<Users>();
                priceList.PriceListProducts = new List<PriceListEntity>();
                priceList.Companies = new List<Logic.Models.Company>();

                await priceListController.PostPriceList(priceList);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(priceList));
        }
        #endregion

        #region GET requests test

        [Fact, AttributePriority(0)]
        public async Task GetListsOfPriceLists()
        {
            List<PriceList> priceLists = new List<PriceList>();
            try
            {
                priceLists = await priceListController.GetListOfPriceList();
                Assert.NotNull(priceLists);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(priceLists));
        }
        [Theory, AttributePriority(1)]
        [MemberData(nameof(PriceListIds))]
        public async Task GetPriceListById(int id)
        {
            PriceList priceList = new PriceList();
            try
            {
                priceList = await priceListController.GetPriceList(id);
                Assert.NotNull(priceList);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(priceList));
        }
        #endregion

        #region PUT requeset test
        [Theory, AttributePriority(5)]
        [MemberData(nameof(PriceListUpdateId))]
        public async Task UpdatePriceList(int id)
        {
            PriceList priceList = new PriceList();
            try
            {
                priceList = await dataCollection.PriceList.GetById(id);
                Random rand = new Random();
                priceList.Name = $"Name{rand.Next(100, 999)}";
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(priceList));
        }
        #endregion

        #region DELETE request
        [Theory, AttributePriority(3)]
        [MemberData(nameof(PriceListIdsForDelete))]
        public async Task DeleteUser(int id)
        {
            PriceList priceList = await dataCollection.PriceList.GetById(id);
            if (priceList == null)
            {
                Assert.True(priceList == null);
            }

            await priceListController.DeletePriceList(priceList.Id);

            output.WriteLine(JsonSerializer.Serialize(priceList));
            output.WriteLine($"{priceList.Name} has been sent to the void");
        }
        #endregion
    }
}
