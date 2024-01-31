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
            return await context.PriceList.FirstOrDefaultAsync(priceList => priceList.Id == id);
        }

        
        public async Task<List<Products>> UpdateListOfProductsWithPricesFromPriceList(List<Products> products,Users? user)
        {

            if (user != null)
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

            return products;
        }
        public async Task<List<PriceList?>> GetListOfPriceList()
        {
            try
            {
                return await context.PriceList.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }
}
