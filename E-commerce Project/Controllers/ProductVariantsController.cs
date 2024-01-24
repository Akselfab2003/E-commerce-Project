﻿using E_commerce.Logic.Interfaces.Table_Interfaces;
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
    public class ProductVariantsController : Controller
    {
        IProductVariants context;
        IDataCollection DataContext;
        public ProductVariantsController(IDataCollection c) { context = c.ProductVariants; DataContext = c; } // Dependency Injection - DI

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

        [HttpGet("")]
        public async Task<List<ProductVariants>?> GetProductVariants()
        {
            var productVariants = await context.GetList();

            if (productVariants == null)
            {
                return null;
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
        public async Task<ActionResult<ProductVariants>> PostProductVariants(ProductVariants productVariants, int ID)
        {
            productVariants.ParentProduct = await DataContext.Products.GetById(ID);
            await context.Create(productVariants);
            return CreatedAtAction("GetProductVariants", new { id = productVariants.Id }, productVariants);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteProductVariants(int id)
        {
            var productVariants = await context.GetById(id);
            if (productVariants == null)
            {
                return HttpStatusCode.BadRequest;
            }

            context.Delete(productVariants);

            return HttpStatusCode.Created;
        }
    }
}
