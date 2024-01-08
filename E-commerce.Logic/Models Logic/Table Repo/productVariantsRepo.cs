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
    public class productVariantsRepo : IProductVariants
    {
        DBcontext context;
        public productVariantsRepo(DBcontext c) { context = c; }
        public async Task<ProductVariants> CreateProductVariants(ProductVariants entity)
        {
            context.ProductVariants.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteProductVariants(int id)
        {
            try
            {
                ProductVariants productVariant = await GetById(id);
                context.ProductVariants.Remove(productVariant);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<ProductVariants> GetById(int id)
        {
            return await context.ProductVariants.FirstOrDefaultAsync(productVariant => productVariant.Id == id);
        }

        public async Task<ProductVariants> GetByName(string name)
        {
            return await context.ProductVariants.FirstOrDefaultAsync(productVariant => productVariant.Name == name);
        }

        public async Task<ProductVariants> UpdateProductVariants(ProductVariants entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
