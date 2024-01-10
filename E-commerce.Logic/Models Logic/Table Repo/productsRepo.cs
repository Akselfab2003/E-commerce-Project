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
    public class productsRepo : IProducts
    {
        DBcontext context;
        public productsRepo(DBcontext c) { context = c; }

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

        public async Task<Products> GetById(int id)
        {
            return await context.Products.Include(product => product.Images).FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<Products> GetByName(string name)
        {
            return await context.Products.FirstOrDefaultAsync(product => product.Title == name);
        }

        public async Task<Products> UpdateProduct(Products entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
