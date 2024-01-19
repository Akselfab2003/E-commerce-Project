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
    public class ReviewsRepo : GenericRepo<Reviews>, IReviews
    {
        DBcontext Context;
        public ReviewsRepo(DBcontext context) : base(context)
        {

            Context = context;

        }
        public async Task<Reviews> CreateReview(Reviews entity)
        {
            Context.Reviews.Add(entity);

            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteReview(int id)
        {
            try
            {
                Reviews ReviewsEntity = await GetById(id);

                Context.Reviews.Remove(ReviewsEntity);

                await Context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }


            return true;
        }

     

        public async Task<Reviews> GetById(int id)
        {
            return await Context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

    
        public async Task<Reviews> UpdateReview(Reviews entity)
        {
           
            Context.Reviews.Update(entity);
            await Context.SaveChangesAsync();

            return entity;

        
        }
    }
}
