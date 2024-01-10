using E_commerce.Logic.Interfaces;
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
        public async Task Can_create_userAsync()
        {
            Users users = new Users();
            users.Gender = true;
            users.Id = 1;
            users.Email = "test";
            users.Session = new Session() { Id = 1,SessId = "test"};
            users.Username = "test";
            users.Password = "test";

            await dataCollection.Users.CreateUser(users);

            Users userreturned = await dataCollection.Users.GetById(1);

            output.WriteLine(JsonSerializer.Serialize(userreturned));

            Assert.Equal(userreturned,users);
        }
    }
}
