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
        public Task<Users> CreateUser(Users entity);
        public Task<Users> UpdateUser(Users entity);
        public Task<bool> DeleteUser(int id);
        public Task<Users> GetById(int id);
    }
}
