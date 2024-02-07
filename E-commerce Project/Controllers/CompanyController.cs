using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        ICompany context;
        private IDataCollection dataCollection;
        public CompanyController(IDataCollection c)
        {
            context = c.Company;
            dataCollection = c;
        }

        #region GET Requests
        [HttpGet("GetAllCompanies")]
        public async Task<List<Company>> GetCompanies()
        {
            return await context.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<Company> GetCompanyById(int id)
        {
            var company = await context.GetById(id);

            if (company == null)
            {
                return null;
            }

            return company;
        }
        #endregion

        #region POST Requests
        [HttpPost("CreateCompany")]
        public async Task<HttpStatusCode> PostCompany(Company company)
        {
            try
            {
                //await dataCollection.Company.Create(company);
                await context.Create(company);
            }
            catch(Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.Created;
        }
        #endregion

        #region PUT Requests
        [HttpPut("UpdateCompany")]
        public async Task<HttpStatusCode> PutCompany(Company company)
        {
            try
            {
                await context.Update(company);
            }
            catch (DbUpdateConcurrencyException)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }
        #endregion

        #region DELETE Requests
        [HttpPost("DeleteCompany")]
        public async Task<HttpStatusCode> DeleteCompany(Company company)
        {
            try
            {
                await context.Delete(company);
                return HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }
        #endregion
    }
}
