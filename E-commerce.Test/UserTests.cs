using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace E_commerce.Test
{
    [Collection("Services")]
    public class UserTests
    {

        private readonly IDataCollection dataCollection;

        private readonly ITestOutputHelper output;
        public UserTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
        }


        [Fact]
        public async Task PostEmptySessionTest()
        {

            Session session1 = new Session();
            try
            {
                session1.user = null;

                await dataCollection.Session.CreateSession(session1);
                Basket basket = new Basket();
                basket.Session = session1;
                await dataCollection.Basket.CreateBasket(basket);
                Assert.NotNull(session1);
                Assert.NotNull(basket);
            }
            catch(Exception ex) 
            {
              
            }
         

            Assert.True(session1.SessId.Length > 0,"sessid does not exists");

            output.WriteLine(JsonSerializer.Serialize(session1));
                


        }


    }
}
