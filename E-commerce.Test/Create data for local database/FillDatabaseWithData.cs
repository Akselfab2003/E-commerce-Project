using Bogus;
using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace E_commerce.Test.Create_data_for_local_database
{
    [Collection("LocalDatabaseService")]
    public class FillDatabaseWithData
    {
        private readonly ITestOutputHelper output;
        private  IDataCollection DataCollection;

        public FillDatabaseWithData(GenerateFakeDataForDatabase collection,ITestOutputHelper outputHelper) 
        {
            this.output = outputHelper;
            this.DataCollection = collection.DataCollection;
        
        }

        [Fact]
        public async void InsertDataToDatabase()
        {
            Faker<Users> faker = new Faker<Users>()
                //.RuleFor(user => user.Id, data => data.IndexFaker)
                .RuleFor(user => user.Username, data => data.Person.UserName)
                .RuleFor(user => user.Password, data => data.Internet.Password(10))
                .RuleFor(user => user.Email, data => data.Person.Email)
                .RuleFor(user => user.Gender, data => data.Random.Bool());


            List<Users> data = faker.GenerateBetween(10, 20);

            output.WriteLine(JsonSerializer.Serialize(data));
            foreach (Users user in data)
            {
                await DataCollection.Users.CreateUser(user);
            }
            Assert.True(data.Any());
            
        }
    }
}
