using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        IProducts context;
        private IDataCollection dataCollection;
        public ProductsController(IDataCollection c) 
        {
            context = c.Products;
            dataCollection = c;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductById(int id)
        {
            var product = await context.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("GetLimitedAmountOfProducts/{sessid}")]
        public async Task<List<Products>> GetLimitedAmountOfProducts(string sessid = "")
        {
            // var cookies = Request.Cookies;
            Users? users = null;
            try
            {

            }
            catch (Exception ex)
            {
                users = (await dataCollection.Session.GetById(sessid)).user;

            }

            //if (cookies != null && cookies.Count()>0 | sessid != "")
            //{

            //   cookies.TryGetValue("sessionId", out sessid);

            // }

            List<Products> products = await dataCollection.PriceList.UpdateListOfProductsWithPricesFromPriceList(await context.GetProducts(40), users);

            return products;
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<Products>?> GetAllProducts()
        {
            // var cookies = Request.Cookies;
            Users? users = null;
            try
            {
            List<Products> products =await context.GetAllProducts();

            return products;
            }
            catch (Exception ex)
            {
                return null;

            }

        }


        [HttpPost("GetProductsThatArePartOfCategory")]
        public async Task<List<Products>> GetProductsPartOfCategory(int CategoryId, int[] Productsids,string sessid)
        {

            Categories category = await dataCollection.Categories.GetById(CategoryId);
            Users? users = (await dataCollection.Session.GetById(sessid)).user;
            return await dataCollection.PriceList.UpdateListOfProductsWithPricesFromPriceList(await dataCollection.Categories.GetProductsFromCategory(category, Productsids), users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }

            try
            {
                
                await context.UpdateProduct(products);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Products>> PostProduct(Products product)
        {
            try
            {
                product.ProductCategories = await dataCollection.Categories.GetById(product.ProductCategories.Id);
                await context.Create(product);
            }
            catch (Exception ex)
            {
            }

            return CreatedAtAction("GetCategories", new { id = product.Id }, product);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await context.DeleteProduct(product);

            return NoContent();
        }
    }
}
