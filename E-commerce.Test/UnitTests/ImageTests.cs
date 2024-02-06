using Bogus.DataSets;
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
    public class ImageTests
    {
        private readonly IDataCollection dataCollection;

        private ImageController imageController;

        private readonly ITestOutputHelper output;
        public ImageTests(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            imageController = new ImageController(dataCollection);
            output = outputHelper;
            InsertTestData();
        }
        #region DataParameters
        public static IEnumerable<Object[]> ImageId()
        {
            yield return new object[] { 1000 };
        }
        public static IEnumerable<Object[]> ImageObject()
        {
            yield return new object[] { new Logic.Models.Images() { Id=9,ImagePath="Update"} };
        }
        #endregion

        #region InsertData

        public void InsertTestData()
        {
            for (int i = 1; i <= 5; i++)
            {
                Logic.Models.Images images = new Logic.Models.Images() { ImagePath = $"{i}"};
                dataCollection.Images.Create(images).Wait();
            }
            Logic.Models.Images image = new Logic.Models.Images() {Id=1000, ImagePath = "LOL.png" };
            dataCollection.Images.Create(image).Wait();
        }
        #endregion

        #region GET requests test
        [Fact, AttributePriority(0)]
        public async Task GetAllImages()
        {
            List<Logic.Models.Images> images = new List<Logic.Models.Images>();
            try
            {
                images = await imageController.GetAllImages();
                Assert.NotNull(images);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(images));
        }
        #endregion

        #region POST requests test
        [Fact, AttributePriority(1)]
        public async Task CreateImage()
        {
            Logic.Models.Images image = new Logic.Models.Images();
            try
            {
                image.ImagePath = "Hit";

                await imageController.PostImage(image);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(image));
        }
        #endregion

        #region DELETE request
        [Theory, AttributePriority(2)]
        [MemberData(nameof(ImageId))]
        public async Task DeleteImage(int id)
        {
            Logic.Models.Images image = await dataCollection.Images.GetById(id);
            if (image == null)
            {
                Assert.True(image == null);
            }

            await imageController.DeleteImages(image.Id);

            output.WriteLine(JsonSerializer.Serialize(image));
            output.WriteLine($"{image.ImagePath} has been shredded");
        }
        #endregion

        #region PUT request
        [Theory, AttributePriority(3)]
        [MemberData(nameof(ImageObject))]
        public async Task UpdateImage(Logic.Models.Images image)
        {
            List<Logic.Models.Images> images = [];
            images = await dataCollection.Images.GetAllImages();
            Random random = new Random();
            imageController.PutImage(images[random.Next(1,4)].Id, image);
        }
        #endregion
    }
}