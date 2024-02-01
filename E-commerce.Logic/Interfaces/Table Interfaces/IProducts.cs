using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IProducts : IGeneric<Products>
    {
        public Task<Products> CreateProduct(Products entity);
        public Task<Products> UpdateProduct(Products entity);
        public Task<bool> DeleteProduct(Products entity);
        public Task<Products> GetById(int id);
        public Task<List<Products>> GetProducts(int count);
        public Task<List<Products>?> GetAllProducts();
        public Task<List<Products>> SearchForProducts(string SearchInput);

    }
}
