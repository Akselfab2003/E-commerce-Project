using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class PriceListRepo : GenericRepo<PriceList>, IPriceList
    {
        DBcontext context;
        public PriceListRepo(DBcontext c) : base(c)
        {
            context = c;
        } 

      

        public async Task<PriceList> GetById(int id)
        {
            return await context.PriceList
                .Include(ele=>ele.Companies)
                .Include(ele=>ele.Users)
                .Include(ele=>ele.PriceListProducts)
                .ThenInclude(ele=>ele.Product)
                .ThenInclude(ele=>ele.ProductCategories)
                .Include(ele => ele.PriceListProducts)
                .ThenInclude(ele => ele.Product)
                .ThenInclude(ele => ele.Images)
                .FirstOrDefaultAsync(priceList => priceList.Id == id);
        }

        
        public async Task<List<Products>> UpdateListOfProductsWithPricesFromPriceList(List<Products> products,Users? user)
        {

            if (user != null)
            {

                int count = (context.PriceList.ToList()).Count();
                if (count > 0)
                {

                
                PriceList priceList = await context.PriceList.Where(priceList => priceList.Users.Contains(user) || priceList.Companies.Contains(user.Company == null ? new Company() : user.Company)).FirstAsync();

                List<Products> ProductsWithNewPrices = new List<Products>();
                if (priceList != null)
                {
                    foreach (Products Product in products)
                    {
                        if (priceList.PriceListProducts.Any(ProductFromPriceList => ProductFromPriceList.Product.Id == Product.Id))
                        {
                            ProductsWithNewPrices.Add(priceList.PriceListProducts.Where(ProductFromPriceList => ProductFromPriceList.Product.Id == Product.Id).First().Product);
                        }
                        else
                        {
                            ProductsWithNewPrices.Add(Product);
                        }
                    }
                }

                return ProductsWithNewPrices;
                }
            }

            return products;
        }
        public async Task<List<PriceList?>> GetListOfPriceList()
        {
            try
            {
                return await context.PriceList
                    .Include(ele => ele.Companies)
                    .Include(ele => ele.Users)
                    .Include(ele => ele.PriceListProducts)
                    .ThenInclude(ele => ele.Product)
                    .ThenInclude(ele => ele.ProductCategories)
                    .Include(ele => ele.PriceListProducts)
                    .ThenInclude(ele => ele.Product)
                    .ThenInclude(ele => ele.Images)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<List<Products>> GetProductsNotPartOfPriceList(int id)
        {
            try
            {
                List<Products> products = new List<Products>();
                var pricelist = await GetById(id);
                foreach (var product in (await context.Products.ToListAsync())) 
                {
                    if (pricelist.PriceListProducts.Any(c => c.Product == product))
                    {
                        products.Add(product);
                    }
                }
                //return await context.Products.Where(ele=>pricelist.PriceListProducts.Where(c=>c.Product.Equals(ele)).Count()==0).ToListAsync();
                return products;
            }
            catch (Exception ex) 
            {
            }
            return new List<Products>();
        }
        public async Task<List<Users>> GetUsersNotPartOfPriceList(int id)
        {
            try
            {
                var pricelist = await GetById(id);
                return await context.Users.Where(ele => pricelist.Users.Any(c => c.Equals(ele))).ToListAsync();
            }
            catch (Exception ex)
            {
            }
            return new List<Users>();
        }
        public async Task<List<Company>> GetCompaniesNotPartOfPriceList(int id)
        {
            try
            {
                var pricelist = await GetById(id);
                return await context.Company.Where(ele => pricelist.Companies.Any(c => c.Equals(ele))).ToListAsync();
            }
            catch (Exception ex)
            {
            }
            return new List<Company>();
        }

    }
}
