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
    public class productVariantsRepo : GenericRepo<ProductVariants>, IProductVariants
    {
        DBcontext context;
        public productVariantsRepo(DBcontext c) : base(c)
        {
            context = c;
        }
        public async Task<ProductVariants> CreateProductVariants(ProductVariants entity)
        {
            context.ProductVariants.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteProductVariants(ProductVariants entity)
        {
            try
            {
                ProductVariants productVariant = await GetById(entity.Id);
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

        public async Task<List<ProductVariants>> GetList()
        {
            return await context.ProductVariants.ToListAsync();
        }
        public async Task<List<ProductVariants>> GetListOfProductVariantsByProductId(int ProductId)
        {
            return await context.ProductVariants.Include(ele => ele.ParentProduct).Where(variant => variant.ParentProduct.Id == ProductId).ToListAsync();
        }

        public async Task<ProductVariants> UpdateProductVariants(ProductVariants entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
