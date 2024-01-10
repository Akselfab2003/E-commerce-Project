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
        public Task<Products> CreateProduct(Products entity);
        public Task<Products> UpdateProduct(Products entity);
        public Task<bool> DeleteProduct(Products entity);
        public Task<Products> GetById(int id);
        public Task<Products> GetByName(string name);
    }
}
