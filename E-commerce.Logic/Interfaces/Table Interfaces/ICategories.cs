using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface ICategories
    {
        public Task<Categories> CreateCategories(Categories entity);
        public Task<Categories> UpdateCategories(Categories entity);
        public Task<bool> DeleteCategories(Categories entity);
        public Task<Categories> GetById(int id);
        public Task<List<Categories>> GetCategoriesForListOfIds(List<int> id);

        public Task<List<Categories>> GetAllUniqueCategories();

    }
}
