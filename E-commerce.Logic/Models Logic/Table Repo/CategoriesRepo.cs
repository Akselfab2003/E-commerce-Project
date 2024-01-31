using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class CategoriesRepo : GenericRepo<Categories>, ICategories
    {
        DBcontext Context;
        public CategoriesRepo(DBcontext context) : base(context)
        {
            Context = context;   
        }
        public async Task<Categories> CreateCategories(Categories entity)
        {
            Context.Categories.Add(entity);

            await Context.SaveChangesAsync();

            return entity;

        }

        public async Task<bool> DeleteCategories(Categories entity)
        {
            try
            {
                Categories CategoriesEntity = await GetById(entity.Id);

                Context.Categories.Remove(CategoriesEntity);
                Products products= await Context.Products.FirstOrDefaultAsync(product => product.ProductCategories == CategoriesEntity);
                if (products != null)
                {
                    products.ProductCategories = null;
                    Context.Products.Update(products);
                }
                await Context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }


            return true;
        }


        public async Task<Categories> GetById(int id)
        {
            return await Context.Categories.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Categories>> GetCategoriesForListOfIds(List<int> id)
        {

            return await Context.Categories.Where(Tag => id.Contains(Tag.Id)).ToListAsync();

        }
        public async Task<List<Categories>> GetAllUniqueCategories()
        {
            List<Categories> Categories = await Context.Categories.ToListAsync();

            return Categories.DistinctBy(t => t.Name).ToList();


        }

        //public async Task<List<Products>> GetProductsFromCategory(Categories category)
        //{
        //    List<Products> Products = await Context.Products.Include(Cat => Cat.ProductCategories).Include(ele => ele.Images).Where(ele => ele.ProductCategories == category).ToListAsync();

        //    return Products;


        //}


        public async Task<List<Products>> GetProductsFromCategory(Categories category, int[] ProductIds)
        {
            // List<Products> Products = await Context.Products.Include(Cat => Cat.ProductCategories).Include(ele => ele.Images).Where(ele => ele.ProductCategories == category).ToListAsync();
            List<Products> Products = await Context.Products.Include(product => product.Images)
                .Include(product => product.ProductCategories)
                .Include(product => product.ProductVariants).Where(product => ProductIds.Contains(product.Id) && product.ProductCategories == category ).ToListAsync();
            return Products;


        }

        public async Task<Categories> UpdateCategories(Categories entity)
        {
            Context.Categories.Update(entity);

            await Context.SaveChangesAsync();

            return entity;
        }



    }
}
