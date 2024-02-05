using E_commerce.Logic.Interfaces.Table_Interfaces;
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

        #region GET Requests
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

        [HttpGet("GetProductVariants/{productid}")]
        public async Task<List<ProductVariants>?> GetProductVariants(int productid)
        {
            var productVariants = await context.GetListOfProductVariantsByProductId(productid);

            if (productVariants == null)
            {
                return null;
            }

            return productVariants;
        }
        #endregion

        #region POST Requests
        [HttpPost]
        public async Task<HttpStatusCode> PostProductVariants(ProductVariants productVariants, int ID)
        {
            productVariants.ParentProduct = await DataContext.Products.GetById(ID);
            await context.Create(productVariants);
            return HttpStatusCode.Created;
        }
        #endregion

        #region PUT Requests
        [HttpPut("{id}")]
        public async Task<HttpStatusCode> PutProductVariants(int id, ProductVariants productVariantss)
        {
            if (id != productVariantss.Id)
            {
                return HttpStatusCode.BadRequest;
            }

            try
            {
                await context.UpdateProductVariants(productVariantss);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return HttpStatusCode.OK;
        }
        #endregion

        #region DELETE Requests
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteProductVariants(int id)
        {
            var productVariants = await context.GetById(id);
            if (productVariants == null)
            {
                return HttpStatusCode.BadRequest;
            }

            context.Delete(productVariants);

            return HttpStatusCode.NoContent;
        }
        #endregion
    }
}
