using E_commerce.Logic.Interfaces;
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
        public ProductsController(IDataCollection c) { context = c.Products; dataCollection = c; } // Dependency Injection - DI

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

        [HttpGet("GetLimitedAmountOfProducts")]
        public async Task<List<Products>> GetLimitedAmountOfProducts()
        {
            return await context.GetProducts(40);
        }

        [HttpPost("GetProductsThatArePartOfCategory")]
        public async Task<List<Products>> GetProductsPartOfCategory(int CategoryId, int[] Productsids)
        {

            Categories category = await dataCollection.Categories.GetById(CategoryId);
            return await dataCollection.Categories.GetProductsFromCategory(category,Productsids);
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

        [HttpPost]
        public async Task<ActionResult<Products>> PostProduct(Products product)
        {
            await context.CreateProduct(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
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

            context.DeleteProduct(product);

            return NoContent();
        }
    }
}
