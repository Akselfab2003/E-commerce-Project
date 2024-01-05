using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IUsers
    {
        public Task<Users> Create(Users entity);
        public Task<Users> Update(Users entity);
        public Task<bool> Delete(Users entity);
        public Task<Users> Get(Users entity);
        public Task<Users> GetById(int id);
        public Task<Users> GetByName(string name);
    }
}
