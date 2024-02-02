using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Logic.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private readonly IPriceList priceList;
        private readonly IDataCollection collection;
        public PriceListController(IDataCollection _context)
        {
            priceList = _context.PriceList;
            collection = _context;
        }
        #region POST Requests
        [HttpPost]
        public async Task<HttpStatusCode> PostPriceList(PriceList priceList)
        {
            try
            {

                await collection.PriceList.Create(priceList);
            }
            catch (Exception ex)
            {

                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        #endregion

        #region PUT Requests
        [HttpPut("UpdatePriceList/{id}")]
        public async Task<PriceList?> UpdateUser(int id,PriceList priceList)
        {
            //Users user=
            if (id == priceList.Id)
            {
                try
                {
                    return await collection.PriceList.Update(priceList);
                }
                catch (Exception ex)
                {

                }
            }


            return null;
        }
        #endregion

        #region GET Requests
        [HttpGet("GetListOfPriceList")]
        public async Task<List<PriceList?>> GetListOfPriceList()
        {
            try
            {
                return await priceList.GetListOfPriceList();

            }
            catch
            {
                return null;
            }

        }
        [HttpGet("PriceList/{id}")]
        public async Task<IActionResult> GetPriceList(int id)
        {
            try
            {
                var priceList = await collection.PriceList.GetById(id);
                return Ok(priceList);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }
        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetPriceListProduct(int id)
        {
            try
            {
                var priceList = await collection.PriceList.GetProductsNotPartOfPriceList(id);
                return Ok(priceList);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }
        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetPriceListUsers(int id)
        {
            try
            {
                var priceList = await collection.PriceList.GetUsersNotPartOfPriceList(id);
                return Ok(priceList);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }
        [HttpGet("Companies/{id}")]
        public async Task<IActionResult> GetPriceListCompanies(int id)
        {
            try
            {
                var priceList = await collection.PriceList.GetCompaniesNotPartOfPriceList(id);
                return Ok(priceList);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }
        #endregion

        #region DELETE Requests
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceList(int id)
        {
            var pricelist = await priceList.GetById(id);
            if (pricelist == null)
            {
                return NotFound();
            }

            await collection.PriceList.Delete(pricelist);

            return NoContent();
        }
        #endregion
    }
}
