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
    public class TagsRepo : ITags
    {
        DBcontext Context;
        public TagsRepo(DBcontext context)
        {
           Context = context;
        }

        public async Task<Tags> CreateTags(Tags entity)
        {
            Context.Tags.Add(entity);

            await Context.SaveChangesAsync();

            return entity;

        }

        public async Task<bool> DeleteTags(Tags entity)
        {
            try
            {
                Tags TagsEntity = await GetById(entity.Id);

                Context.Tags.Remove(TagsEntity);

                await Context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }


            return true;
        }

       
        public async Task<Tags> GetById(int id)
        {
            return await Context.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tags>> GetTagsForListOfIds(List<int> id)
        {

            return await Context.Tags.Where(Tag => id.Contains(Tag.Id)).ToListAsync();

        }
        public async Task<List<Tags>> GetAllUniqueTags()
        {
            List<Tags> tags = await Context.Tags.ToListAsync();

            return tags.DistinctBy(t => t.Name).ToList()    ;


        }
        public async Task<Tags> UpdateTags(Tags entity)
        {
            Context.Tags.Update(entity);

            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
