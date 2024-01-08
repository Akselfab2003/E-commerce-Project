using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IReviews
    {
        public Task<Reviews> CreateReview(Reviews entity);
        public Task<Reviews> UpdateReview(Reviews entity);
        public Task<bool> DeleteReview(int id);
        public Task<Reviews> GetById(int id);
    }
}
