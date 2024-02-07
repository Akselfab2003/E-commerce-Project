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

        // Constructor for the CompanyTest class, taking dependencies for data collection and output helper
        public CompanyTest(CreateFakeDBDependencies collection, ITestOutputHelper outputHelper)
        {
            dataCollection = collection.DataCollection;
            output = outputHelper;
            companyController = new CompanyController(dataCollection);
            GenerateTestCompanies().Wait();
        }

        // Method to asynchronously generate test companies and post them to the database
        public async Task GenerateTestCompanies()
        {
            Company data = (await dataGenerator.CreateCompany())[0];
            await companyController.PostCompany(data);
            //await dataCollection.Company.Create(data);
        }

        public static IEnumerable<Object[]> CompanyNameTestData()
        {
            yield return new object[] { "CompanyTest" };
        }

        #region GetTest
        [Fact]
        public async Task GetAllCompaniesTest()
        {
            List<Company> company = await companyController.GetCompanies();
            Assert.NotNull(company);
        }

        [Fact]
        public async Task GetCompanyById()
        {
            Company companyID = await companyController.GetCompanyById(1);
            output.WriteLine(JsonSerializer.Serialize(companyID));
            Assert.NotNull(companyID);
        }
        #endregion

        #region PostTest
        [Fact]
        public async Task CreateCompanyTest()
        {
            Company company = new Company();
            try
            {
                company.cvr = "2334556";
                company.Name = "CompanyTest";
                company.email = "Test@gmail.com";

                await companyController.PostCompany(company);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            output.WriteLine(JsonSerializer.Serialize(company));
        }
        #endregion

        #region PutTest
        [Fact]
        public async Task UpdateCompanyTest()
        {
            // Getting a company by its ID
            Company company = await companyController.GetCompanyById(1);
            // Asserting that the company name is not "Update"
            Assert.True(company.Name != "Update");

            // Updating the company name
            company.Name = "Update";
            await companyController.PutCompany(company);

            // Getting the updated company by its ID
            Company company2 = await companyController.GetCompanyById(company.Id);

            // Writing the updated company to the output
            output.WriteLine(JsonSerializer.Serialize(company2));
            // Asserting that the company name is now "Update"
            Assert.True(company2.Name == "Update", "Update product Failed");

        }
        #endregion

        #region DeleteTest
        [Theory]
        [MemberData(nameof(CompanyNameTestData))]
        public async Task DeleteCompanyTest(string name)
        {
            // Getting a company by its name
            Company company = await dataCollection.Company.GetByName(name);
            // Asserting that the company is not null
            if (company == null)
            {
                Assert.True(company == null);
            }
            // Deleting the company
            await companyController.DeleteCompany(company);

            // Attempting to get the deleted company by its name
            Company company2 = await dataCollection.Company.GetByName(name);

            //output.WriteLine(JsonSerializer.Serialize(company));
            Assert.Null(company2);
            //output.WriteLine($"{company2.Name}) has been deleted");
        }
        #endregion
    }
}
