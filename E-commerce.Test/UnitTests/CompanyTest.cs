using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Test.Create_data_for_local_database;
using E_commerce.Test.UnitTest_Database_Setup;
using E_commerce_Project.Controllers;
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
    public class CompanyTest
    {
        private readonly IDataCollection dataCollection;
        private readonly ITestOutputHelper output;
        private CompanyController companyController;
        private FillDatabaseWithData fillDatabaseWithData;
        private static readonly FakeDataForTest dataGenerator = new FakeDataForTest();

        public CompanyTest(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            companyController = new CompanyController(dataCollection);
        }

        public async void GenerateTestCompanies()
        {
            Company data = (await dataGenerator.CreateCompany())[0];
        }

        #region GetTest
        //[Fact]
        //public async Task GetAllCompaniesTest()
        //{
        //    Company company = await dataCollection.Company.GetAll(company);
        //    Assert.NotNull(company);
        //}

        [Fact]
        public async Task GetCompanyById()
        {
            Company companyID = await companyController.GetCompanyById(1);
            output.WriteLine(JsonSerializer.Serialize(companyID));
            Assert.NotNull(companyID);
        }
        #endregion
    }
}
