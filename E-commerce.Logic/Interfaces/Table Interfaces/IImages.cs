using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface IImages : IGeneric<Images>
    {
        public Task<Images> GetById(int id);
        public Task<Images> UpdateImage(Images image);
        public Task<List<Images>> GetAllImages();
        public Task<bool> DeleteImage(Images image);
        public Task<Images> CreateImage(Images image);
    }
}
