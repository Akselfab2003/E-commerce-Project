using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IProducts
    {
        public Task<Products> Create(Products entity);
        public Task<Products> Update(Products entity);
        public Task<bool> Delete(Products entity);
        public Task<Products> Get(Products entity);
        public Task<Products> GetById(int id);
        public Task<Products> GetByName(string name);
    }
}
