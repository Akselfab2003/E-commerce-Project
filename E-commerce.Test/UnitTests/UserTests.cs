using Bogus.DataSets;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System;
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
    public class UserTests
    {

        private readonly IDataCollection dataCollection;

        private readonly ITestOutputHelper output;
        public UserTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            InsertSessidTestData();


        }
        #region DataParameters
        public static IEnumerable<Object[]> SessionIdTestData()
        {
            yield return new object[] { "Test1" };
            yield return new object[] { "Test2" };
        }
        public static IEnumerable<Object[]> LoginObjectTestData()
        {
            yield return new object[] { new LoginObject() { username = "test", password = "password", sessionId = "Test1" } };
            yield return new object[] { new LoginObject() { username = "test", password = "", sessionId = "Test2" } };
            yield return new object[] { new LoginObject() { username = "", password = "password", sessionId = "Test3" } };
            yield return new object[] { new LoginObject() { username = "test", password = "password", sessionId = "" } };
            yield return new object[] { new LoginObject() { username = "", password = "", sessionId = "" } };
        }
        public static IEnumerable<Object[]> UsernamesTestData()
        {
            yield return new object[] { "DeleteTest" };
        }
        #endregion

        #region InsertData
        public async Task InsertSessidTestData()
        {
            for (int i = 1; i <= 3; i++)
            {
                Session session = new Session() { Created = DateTime.Now, SessId = $"Test{i}", user = null };
                await dataCollection.Session.CreateSession(session);
            }



            //Users deleteUser = new Users() { Username = "DeleteTest", Password = dataCollection.Cryptography.CreateNewPasswordHash("password"), Email = "test@test.com", Gender = true };
            //await dataCollection.Users.CreateUser(deleteUser);
        }

        public async Task InsertUserTestData()
        {


            Users users = new Users() { Username = "test", Password = dataCollection.Cryptography.CreateNewPasswordHash("password"), Email = "test@test.com", Gender = true };
            await dataCollection.Users.CreateUser(users);


        }



        #endregion

        #region GET requests test
        //GET request to create empty session 

        //[Theory]
        //[MemberData(nameof(SessionIdTestData))]
        //public async Task GetSession(string sessionId)
        //{
        //    Session session = new Session();
        //    try
        //    {
        //        session = await dataCollection.Session.GetById(sessionId);
        //        Assert.NotNull(session);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.Message);
        //    }
        //    output.WriteLine(JsonSerializer.Serialize(session));
        //}
        //PUT and GET request to update session to have a user and to validate session of user
        #endregion

        #region POST requests test
        //POST request create user
        //[Fact]
        //public async Task CreateUser()
        //{
        //    Users users = new Users();
        //    try
        //    {
        //        users.Username = "Lars";
        //        users.Password = dataCollection.Cryptography.CreateNewPasswordHash("Test");
        //        users.Email = "test@test.com";
        //        users.Gender = false;

        //        await dataCollection.Users.CreateUser(users);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.Message);
        //    }
        //    output.WriteLine(JsonSerializer.Serialize(users));
        //}
        ////GET request to get session
        //#endregion

        //#region DELETE request
        //[Theory]
        //[MemberData(nameof(SessionIdTestData))]
        //public async Task DeleteUser(string name)
        //{
        //    Session sess = await dataCollection.Session.GetById(name);
        //    Users user = sess.user;

        //    if (user == null)
        //    {
        //        Assert.True(user==null);
        //    }

        //    await dataCollection.Users.DeleteUser(user.Username);

        //    output.WriteLine(JsonSerializer.Serialize(user));
        //    output.WriteLine($"{user.Username} has been sent to the void");
        //}
        #endregion

       #region PUT and GET requeset test
        //[Theory]
        //[MemberData(nameof(LoginObjectTestData))]
        //public async Task PutAndValidateSession(LoginObject loginObject)
        //{
        //    //Put user into Session
        //    Session session = await dataCollection.Session.GetById(loginObject.sessionId);

        //    if (loginObject.sessionId == "")
        //    {
        //         session = await dataCollection.Session.GetById(loginObject.sessionId);
        //        Assert.True(session == null);
        //    }
        //    else
        //    {

        //    try
        //    {
        //        if (session.Created < DateTime.Now)
        //        {


        //            loginObject.password = dataCollection.Cryptography.CreateNewPasswordHash(loginObject.password);

        //            bool PasswordCorrect = await dataCollection.Users.CheckLogin(loginObject);

        //            if (PasswordCorrect)
        //            {
        //                session = await dataCollection.Session.Login(loginObject);
        //            }
        //            else
        //            {
        //                //Assert.NotNull(session.user);
        //            }
        //        }
        //        else
        //        {
        //            //Assert.False(session.Created < DateTime.Now);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Assert.Fail(ex.Message);
        //    }

        //    }
            
        //    //Validate session
        //    //try
        //    //{
        //    //    Session session1 = await dataCollection.Session.GetById(session.SessId);
        //    //    if (session == null || session.user == null)
        //    //    {
        //    //        Assert.False(session == null || session.user == null);
        //    //    }
        //    //    else
        //    //    {
        //    //        Assert.True(session != null || session.user != null);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Assert.Fail(ex.Message);
        //    //}
        //    output.WriteLine(JsonSerializer.Serialize(session));
        //}
        #endregion




        //[Fact]
        //public async Task PostEmptySessionTest()
        //{

        //    Thread.Sleep(5000);

        //    Session session1 = new Session();
        //    session1.Id = 0;
        //    session1.SessId = session1.SessId + "test";
        //    session1.user = null;
        //    try
        //    {


        //        await dataCollection.Session.CreateSession(session1);
        //        Basket basket = new Basket();
        //        basket.Session = session1;
        //        await dataCollection.Basket.CreateBasket(basket);
        //        Assert.NotNull(session1);
        //        Assert.NotNull(basket);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.Message);
        //    }
        //    Assert.True(session1.SessId.Length > 0, "sessid does not exists");

        //    output.WriteLine(JsonSerializer.Serialize(session1));
        //}
    }
}
