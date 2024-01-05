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
        public Task<Reviews> Create(Reviews entity);
        public Task<Reviews> Update(Reviews entity);
        public Task<bool> Delete(Reviews entity);
        public Task<Reviews> Get(Reviews entity);
        public Task<Reviews> GetById(int id);
        public Task<Reviews> GetByName(string name);
    }
}
