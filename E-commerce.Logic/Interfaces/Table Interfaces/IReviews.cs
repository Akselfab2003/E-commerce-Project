using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IReviews : IGeneric<Reviews>
    {
        public Task<Reviews> GetById(int id);
        public Task<List<Reviews>> GetByProductId(int id);
    }
}
