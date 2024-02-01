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

        #region GET Requests
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
            Users? users = null;
            try
            {
                if (sessid == "")
                {
                    users = (await dataCollection.Session.GetById(sessid)).user;

                }
            }
            catch (Exception ex)
            {

            }

            List<Products> products = await dataCollection.PriceList.UpdateListOfProductsWithPricesFromPriceList(await context.GetProducts(40), users);

            return products;
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<Products>?> GetAllProducts()
        {
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

        [HttpGet("Search/")]
        public async Task<List<Products>> SearchForProducts(string SearchInput, string sessid)
        {
            try
            {
                Users? users = null;
                try
                {

                        users = (await dataCollection.Session.GetById(sessid)).user;
                }
                catch (Exception ex)
                {

                }

                return await dataCollection.PriceList.UpdateListOfProductsWithPricesFromPriceList(await dataCollection.Products.SearchForProducts(SearchInput), users);

            }
            catch (Exception ex)
            {
                return new List<Products> { };
            }
        }
        #endregion

        #region POST Requests
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Products>> PostProduct(Products product)
        {
            try
            {
                product.ProductCategories = await dataCollection.Categories.GetById(product.ProductCategories.Id);
                
                List<Images> images = new List<Images>(); //(await dataCollection.Images.GetAllImages()).Where(ele => product.Images.Contains(ele)).ToList();
                foreach (var item in product.Images)
                {
                    item.Id = 0;

                    images.Add(item);
                }

                product.Images = images;

                await context.Create(product);
            }
            catch (Exception ex)
            {
            }

            return CreatedAtAction("GetCategories", new { id = product.Id }, product);
        }
        #endregion

        #region PUT Products
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
        #endregion

        #region DELETE Requests
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await context.Delete(product);

            return NoContent();
        }
        #endregion
    }
}
