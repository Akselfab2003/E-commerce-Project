using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Net;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasket context;
        private readonly IDataCollection dataCollectioncontext;
        public BasketController(IDataCollection c) 
        { 
            context = c.Basket;
            dataCollectioncontext = c;
        }

        #region GET Requests
        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasketById(int id)
        {
            var basket = await context.GetById(id);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        [HttpGet("GetBasket/{sessid}")]
        public async Task<Basket> GetBasketBySessId(string sessid)
        {
            Session session = await dataCollectioncontext.Session.GetById(sessid);
            var basket = await context.GetBySessId(session);
            if (basket == null)
            {
                return null;
            }

            return basket;
        }
        #endregion

        #region POST Reqeusts
        [HttpPost("AddToBasket/{sessid}")]
        public async Task<List<BasketDetails>> PostBasket(BasketDetails basketDetails, string sessid)
        {
            BasketDetails test = basketDetails;
            test.Products = basketDetails.Products == new Products() ? null : (await dataCollectioncontext.Products.GetById(basketDetails.Products.Id));
            test.Variant = basketDetails.Variant == null ? null : (await dataCollectioncontext.ProductVariants.GetById(basketDetails.Variant.Id));
            await dataCollectioncontext.BasketDetails.Create(test);
            Basket basket = await GetBasketBySessId(sessid);
            basket.BasketDetails.Add(test);
            await dataCollectioncontext.Basket.Update(basket);

            return basket.BasketDetails;
        }

        [HttpPost("CreateBasket")]
        public async Task<Basket> CreateBasket()
        {
            Basket basket = new Basket();

            basket.BasketDetails = new List<BasketDetails> { };
            basket.Session = new Session();

            await dataCollectioncontext.Basket.Create(basket);

            return basket;
        }

        [HttpPost("RemoveFromBasket/{sessId}")]
        public async Task<List<BasketDetails>> DeleteBasketDetail(BasketDetails basketDetails, string sessId)
        {

            await dataCollectioncontext.BasketDetails.Delete(basketDetails);
            return (await GetBasketBySessId(sessId)).BasketDetails;
        }
        #endregion

        #region PUT Requests
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasket(int id, Basket basket)
        {
            if (id != basket.Id)
            {
                return BadRequest();
            }

            try
            {
                foreach (var item in basket.BasketDetails)
                {
                    await dataCollectioncontext.BasketDetails.Update(item);
                }
               // await context.Update(basket);
            }
            catch (Exception ex)
            {

            }

            return NoContent();
        }
        #endregion

        #region DELETE Requests
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(Basket entity)
        {
            var basket = await context.GetById(entity.Id);
            if (basket == null)
            {
                return NotFound();
            }

            await context.Delete(basket);

            return NoContent();
        }
        #endregion
    }
}
