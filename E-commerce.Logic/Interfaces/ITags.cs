using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface ITags
    {
        public Task<Tags> Create(Tags entity);
        public Task<Tags> Update(Tags entity);
        public Task<bool> Delete(Tags entity);
        public Task<Tags> Get(Tags entity);
        public Task<Tags> GetById(int id);
        public Task<Tags> GetByName(string name);
    }
}
