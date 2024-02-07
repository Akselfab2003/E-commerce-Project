using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Test.UnitTest_Database_Setup;
using E_commerce_Project.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace E_commerce.Test.UnitTests
{
    [TestCaseOrderer("E_commerce.Test.OrderedTest", "E-commerce.Test")]
    [Collection("Services")]
    public class ReviewsTests
    {

        private readonly IDataCollection dataCollection;

        private readonly ITestOutputHelper output;

        private readonly ReviewsController Reviewcontroller;

        private static readonly FakeDataForTest fakeDataForTest = new FakeDataForTest();

        private  readonly FakeDataForTest NonStaticfakeDataForTest = new FakeDataForTest();


        public ReviewsTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            GenerateStuff();
            Reviewcontroller = new ReviewsController(collection.DataCollection);
            GenerateTestData();
        }



        public async void GenerateStuff()
        {

            Products products = (await NonStaticfakeDataForTest.InsertProducts())[0];

            await dataCollection.Products.Create(products);
            //await dataCollection.Session.Create(session1);

        }

        public async void GenerateTestData()
        {
          
            
            
            //Users users = new Users();
            //users.Username = "TestUser";
            //await dataCollection.Users.Create(users);


            //Session session1 = new Session();
            //session1.SessId = "TestSessid1";
            //session1.user = users;


            //Products products  = (await NonStaticfakeDataForTest.InsertProducts())[0];

            //await dataCollection.Products.Create(products);
            ////await dataCollection.Session.Create(session1);

            Reviews reviews = await NonStaticfakeDataForTest.GetReviews();
            reviews.Products = await dataCollection.Products.GetById(1);
            await Reviewcontroller.createReviews(reviews,"Test");
          
            // await Reviewcontroller.createReviews(reviews, "TestSessid1");

        }

        public static Reviews GetReview()
        {
            Reviews reviews = fakeDataForTest.GetReviews().Result;
            return reviews;
        }


        public static IEnumerable<object[]> TestDataForReview()
        {
            yield return new object[] { GetReview(), "TestSessid1" };

        }
        public static IEnumerable<object[]> TestDataGetReviewByProductId()
        {
            yield return new object[] { 1 };
        }


        [Theory]
        [MemberData(nameof(TestDataForReview))]
        public async Task Test_CreateReview(Reviews reviews, string sessId)
        {

         HttpStatusCode review  = await Reviewcontroller.createReviews(reviews, sessId);


         Assert.NotNull(await Reviewcontroller.GetReviewsByProductid(1));

         Assert.Equal(HttpStatusCode.Created, review);
         

        }

        //[Fact]
        //public async Task GetReviewFromProductId()
        //{
            
        //    List<Reviews> review = await dataCollection.Reviews.GetByProductId(1);
        //    output.WriteLine(JsonSerializer.Serialize(await dataCollection.Reviews.GetById(1)));

        //    output.WriteLine(JsonSerializer.Serialize(review));
        //    Assert.True(review.Count() >= 1,"No reviews where found");
        //}
    }
}
