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
        public Task<Tags> CreateTags(Tags entity);
        public Task<Tags> UpdateTags(Tags entity);
        public Task<bool> DeleteTags(Tags entity);
        public Task<Tags> Get(Tags entity);
        public Task<Tags> GetById(int id);
        public Task<Tags> GetByName(string name);
    }
}
