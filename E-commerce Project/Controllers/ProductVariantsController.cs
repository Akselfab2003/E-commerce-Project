using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantsController : Controller
    {
        IProductVariants context;
        public ProductVariantsController(IDataCollection c) { context = c.ProductVariants; } // Dependency Injection - DI

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVariants>> GetProductVariantsById(int id)
        {
            var productVariants = await context.GetById(id);

            if (productVariants == null)
            {
                return NotFound();
            }

            return productVariants;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductVariants(int id, ProductVariants productVariantss)
        {
            if (id != productVariantss.Id)
            {
                return BadRequest();
            }

            try
            {
                await context.UpdateProductVariants(productVariantss);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductVariants>> PostProductVariants(ProductVariants productVariants)
        {
            await context.CreateProductVariants(productVariants);
            return CreatedAtAction("GetProductVariants", new { id = productVariants.Id }, productVariants);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductVariants(int id)
        {
            var productVariants = await context.GetById(id);
            if (productVariants == null)
            {
                return NotFound();
            }

            context.DeleteProductVariants(productVariants);

            return NoContent();
        }
    }
}
