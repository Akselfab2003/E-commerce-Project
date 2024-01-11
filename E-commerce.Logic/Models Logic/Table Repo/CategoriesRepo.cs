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
    public class CategoriesRepo : ICategories
    {
        DBcontext Context;
        public CategoriesRepo(DBcontext context) 
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
        public async Task<Categories> UpdateCategories(Categories entity)
        {
            Context.Categories.Update(entity);

            await Context.SaveChangesAsync();

            return entity;
        }



    }
}
