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
    public class productsRepo : GenericRepo<Products>, IProducts
    {
        DBcontext context;
        public productsRepo(DBcontext c) : base(c) 
        {
            context = c;
        }

        public async Task<Products> CreateProduct(Products entity)
        {
            context.Products.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteProduct(Products entity)
        {
            try
            {
                Products product = await GetById(entity.Id);
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<List<Products>?> GetAllProducts()
        {
            return await context.Products.Include(product => product.Images)
                .Include(product => product.ProductCategories)
                .Include(product => product.ProductVariants)
                .ToListAsync();

        }

        public async Task<Products> GetById(int id)
        {
            return await context.Products
                .Include(product => product.Images)
                .Include(product => product.ProductCategories)
                .Include(product => product.ProductVariants)
                .FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<List<Products>> GetProducts(int count)
        {   
            return await context.Products.Include(ele => ele.Images).Include(ele => ele.ProductCategories).Take(count).ToListAsync(); //context.Products.Include(product => product.Images).Take(count).ToListAsync();
        }

        public async Task<Products> UpdateProduct(Products entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }


        public async Task<List<Products>> SearchForProducts(string SearchInput)
        {
            return await context.Products
                                .Include(ele => ele.ProductVariants)
                                .Include(ele => ele.Images)
                                .Include(ele => ele.ProductCategories).Where(product => product.Title.ToLower().Contains(SearchInput.ToLower()) == true).ToListAsync();
        }
    }
}
