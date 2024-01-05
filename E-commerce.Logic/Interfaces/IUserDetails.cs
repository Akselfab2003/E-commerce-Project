using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IUserDetails
    {
        public Task<UserDetails> Create(UserDetails entity);
        public Task<UserDetails> Update(UserDetails entity);
        public Task<bool> Delete(UserDetails entity);
        public Task<UserDetails> Get(UserDetails entity);
        public Task<UserDetails> GetById(int id);
        public Task<UserDetails> GetByName(string name);
    }
}
