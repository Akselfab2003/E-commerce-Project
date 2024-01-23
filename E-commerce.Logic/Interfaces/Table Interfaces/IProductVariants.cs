using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IProductVariants : IGeneric<ProductVariants>
    {
        public Task<ProductVariants> CreateProductVariants(ProductVariants entity);
        public Task<ProductVariants> UpdateProductVariants(ProductVariants entity);
        public Task<bool> DeleteProductVariants(ProductVariants entity);
        public Task<ProductVariants> GetById(int id);
        public Task<List<ProductVariants>> GetList();
    }
}
