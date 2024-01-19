using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface ITags : IGeneric<Tags>
    {
        public Task<Tags> CreateTags(Tags entity);
        public Task<Tags> UpdateTags(Tags entity);
        public Task<bool> DeleteTags(Tags entity);
        public Task<Tags> GetById(int id);
        public Task<List<Tags>> GetTagsForListOfIds(List<int> id);

        public Task<List<Tags>> GetAllUniqueTags();

    }
}
