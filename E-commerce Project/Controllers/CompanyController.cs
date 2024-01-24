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

        [HttpGet("GetAllCompanies")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await context.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(int id)
        {
            var company = await context.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        [HttpPost("CreateCompany")]
        public async Task<HttpStatusCode> PostCompany(Company company)
        {
            try
            {
                await context.Create(company);
            }
            catch(Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.Created;
        }

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

        [HttpPost("DeleteCompany")]
        public async Task<Company> DeleteCompany(Company company)
        {
            try
            {
                return await context.Delete(company);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
