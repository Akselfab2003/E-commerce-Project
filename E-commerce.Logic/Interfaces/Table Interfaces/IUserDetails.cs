using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IUserDetails : IGeneric<UserDetails>
    {
        public Task<UserDetails> CreateUserDetails(UserDetails entity);
        public Task<UserDetails> UpdateUserDetails(UserDetails entity);
        public Task<bool> DeleteUserDetails(int id);
        public Task<UserDetails> GetById(int id);
    }
}
