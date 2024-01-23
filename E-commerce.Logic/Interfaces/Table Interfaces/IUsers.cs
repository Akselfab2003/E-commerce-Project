using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IUsers : IGeneric<Users>
    {
        public Task<Users> GetByName(string name);
        public Task<Users> GetById(int id);
        public Task<List<Users>> GetListOfUsers();

        public Task<bool> CheckLogin(LoginObject loginObject);
    }
}
