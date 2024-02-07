using Bogus.DataSets;
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
    [TestCaseOrderer("E_commerce.Test.OrderedTest", "E-commerce.Test")]
    [Collection("Services")]
    public class UserTests
    {

        private readonly IDataCollection dataCollection;

        private UserController userController;

        private readonly ITestOutputHelper output;

        public UserTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            userController = new UserController(dataCollection);
            output = outputHelper;
            InsertTestData();
        }
        #region DataParameters
        public static IEnumerable<Object[]> SessionIdTestData()
        {
            yield return new object[] { "Test1" };
            yield return new object[] { "Test2" };
        }
        public static IEnumerable<Object[]> LoginObjectTestDataSuccess()
        {
            yield return new object[] { new LoginObject() { username = "Test", password = "password", sessionId = "Test1" } };
            yield return new object[] { new LoginObject() { username = "Test", password = "password", sessionId = "Test1" } };
        }
        public static IEnumerable<Object[]> LoginObjectTestDataFail()
        {
            yield return new object[] { new LoginObject() { username = "Test", password = "", sessionId = "Test2" } };
            yield return new object[] { new LoginObject() { username = "", password = "password", sessionId = "Test2" } };
            yield return new object[] { new LoginObject() { username = "", password = "", sessionId = "Test1" } };
        }
        public static IEnumerable<Object[]> UsernamesTestData()
        {
            yield return new object[] { "DeleteTest" };
        }
        #endregion

        #region InsertData
      
        public void InsertTestData()
        {
            for (int i = 1; i <= 3; i++)
            {
                Session session = new Session() { Created = DateTime.Now, SessId = $"Test{i}", user = null };
                dataCollection.Session.Create(session).Wait();
            }

            Users users = new Users() { Username = "Test", Password = dataCollection.Cryptography.CreateNewPasswordHash("password"), Email = "test@test.com", Gender = true };
            dataCollection.Users.Create(users).Wait();

            Users deleteUser = new Users() { Username = "DeleteTest", Password = dataCollection.Cryptography.CreateNewPasswordHash("password"), Email = "test@test.com", Gender = true };

            dataCollection.Users.Create(deleteUser).Wait();
        }
        #endregion

        #region GET requests test
        //GET request to create empty session 
        [Fact,AttributePriority(0)]
        public async Task PostEmptySessionTest()
        {

            Session session1 = new Session();
            try
            {
                session1 = await userController.PostEmptySession();

                Basket basket = await dataCollection.Basket.GetBySessId(session1);

                output.WriteLine(JsonSerializer.Serialize(basket));
                Assert.NotNull(session1);
                Assert.NotNull(basket);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.True(session1.SessId.Length > 0, "sessid does not exists");

            output.WriteLine(JsonSerializer.Serialize(session1));
        }

        [Theory,AttributePriority(4)]
        [MemberData(nameof(SessionIdTestData))]
        public async Task GetSession(string sessionId)
        {
            Session session = new Session();
            try
            {
                session = await userController.GetSession(sessionId);
                Assert.NotNull(session);
            }
            catch (Exception ex)
            {
                output.WriteLine(sessionId);
               Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(session));
        }
        //PUT and GET request to update session to have a user and to validate session of user
        #endregion

        #region POST requests test
        //POST request create user
        [Fact,AttributePriority(6)]
        public async Task CreateUser()
        {
            Users users = new Users();
            try
            {
                users.Username = "Lars";
                users.Password = "TestTest";
                users.Email = "test@test.com";
                users.Gender = false;

                await userController.PostUser(users);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(users));
        }
        //GET request to get session
        #endregion

        #region DELETE request
        [Theory,AttributePriority(7)]
        [MemberData(nameof(UsernamesTestData))]
        public async Task DeleteUser(string name)
        {
            Users user = await dataCollection.Users.GetByName(name);
            if (user == null)
            {
                Assert.True(user == null);
            }

            await userController.DeleteUser(user);

            output.WriteLine(JsonSerializer.Serialize(user));
            output.WriteLine($"{user.Username} has been sent to the void");
        }
        #endregion

        #region PUT and GET requeset test
        [Theory]
        [MemberData(nameof(LoginObjectTestDataSuccess))]
        public async Task PutAndValidateSessionSucess(LoginObject loginObject)
        {
            //Put user into Session
            try
            {

                HttpStatusCode statusCode = await userController.PutSession(loginObject);
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Theory]
        [MemberData(nameof(LoginObjectTestDataFail))]
        public async Task PutAndValidateSessionFail(LoginObject loginObject)
        {
            //Put user into Session
            try
            {

                HttpStatusCode statusCode = await userController.PutSession(loginObject);
                Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion
    }
}
