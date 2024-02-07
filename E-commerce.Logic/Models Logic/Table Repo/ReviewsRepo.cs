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

        public async Task<Reviews> GetById(int id)
        {
            return await Context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reviews>> GetByProductId(int id)
        {
            return await Context.Reviews.Where(r => r.Products.Id == id).ToListAsync();
        }
    }
}
