using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

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
        public async Task<Products?> GetProductById(int id)
        {
            try
            {
                var product = await context.GetById(id);
                if (product == null)
                {
                    return null;
                }
                return product;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet("GetLimitedAmountOfProducts/{sessid}")]
        public async Task<List<Products>> GetLimitedAmountOfProducts(int count=40,string sessid = "")
        {
            Users? users = null;
            try
            {
                if (sessid == "")
                {
                    users = (await dataCollection.Session.GetById(sessid))?.user;

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

                        users = (await dataCollection.Session.GetById(sessid))?.user;
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
        //"00:00:00.0018953"


        [HttpGet("SearchTest/")]
        public async Task<TimeSpan?> SearchTest(string SearchInput, string sessid)
        {
            try
            {
                //Users? users = null;
                //try
                //{

                //    users = (await dataCollection.Session.GetById(sessid))?.user;
                //}
                //catch (Exception ex)
                //{

                //}
                TimeSpan test = new TimeSpan();
                for (int i = 0; i < 10; i++)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    List<Products> products = await dataCollection.Products.SearchForProducts(SearchInput);

                    stopwatch.Stop();
                    test = test.Add(stopwatch.Elapsed);
                }

                return (test.Divide(10));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //"00:00:00.6733213"
        //"00:00:00.0029060"

        [HttpGet("SearchTestNewWay/")]
        public async Task<TimeSpan> SearchTestNewWay(string SearchInput, string sessid)
        {
            try
            {
                //Users? users = null;
                //try
                //{

                //    users = (await dataCollection.Session.GetById(sessid))?.user;
                //}
                //catch (Exception ex)
                //{

                //}
                TimeSpan test = new TimeSpan();
                for (int i = 0; i < 10; i++)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    List<Products> products = await dataCollection.Products.SearchForProductsNewWay(SearchInput);
               
                    stopwatch.Stop();
                    test = test.Add(stopwatch.Elapsed);
                }
       
                return (test.Divide(10));
            }
            catch (Exception ex)
            {
                return new TimeSpan();
            }
        }




        //"00:00:00.0029826"


        [HttpGet("SearchTestNewWaysecondWay/")]
        public async Task<TimeSpan> SearchTestNewWaysecondWay(string SearchInput, string sessid)
        {
            try
            {
                //Users? users = null;
                //try
                //{

                //    users = (await dataCollection.Session.GetById(sessid))?.user;
                //}
                //catch (Exception ex)
                //{

                //}
                TimeSpan test = new TimeSpan();
                for (int i = 0; i < 10; i++)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    List<Products> products = await dataCollection.Products.SearchForProductsAsyncTest(SearchInput);
                    stopwatch.Stop();
                    test = test.Add(stopwatch.Elapsed);
                }

                return (test.Divide(10));
            }
            catch (Exception ex)
            {
                return new TimeSpan();
            }
        }
        #endregion

        #region POST Requests
        [HttpPost("CreateProduct")]
        public async Task<HttpStatusCode> PostProduct(Products product)
        {
            try
            {

                if (product.ProductCategories != null)
                {

                product.ProductCategories =  await dataCollection.Categories.GetById(product.ProductCategories.Id);
                
                }
                
                List<Images> images = new List<Images>();
                if(product.Images != null)
                {
                foreach (var item in product.Images)
                {
                    item.Id = 0;

                    images.Add(item);
                }

                product.Images = images;


                }
                
                //(await dataCollection.Images.GetAllImages()).Where(ele => product.Images.Contains(ele)).ToList();
                await context.Create(product);
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        #endregion

        #region PUT Products
        [HttpPut("{id}")]
        public async Task<HttpStatusCode> PutProduct(int id, Products products)
        {
            if (id != products.Id)
            {
                return HttpStatusCode.BadRequest;
            }

            try
            {

                await context.UpdateProduct(products);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return HttpStatusCode.OK;
        }
        #endregion

        #region DELETE Requests
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteProduct(int id)
        {
            var product = await context.GetById(id);
            try
            {

            if (product == null)
            {
                return HttpStatusCode.BadRequest;
            }

            await context.Delete(product);
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.NoContent;
        }
        #endregion
    }
}
