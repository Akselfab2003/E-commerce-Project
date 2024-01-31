using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Logic.Models;
using System.Net;

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
    }
}
