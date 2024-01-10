using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IAdminUsers
    {
        public Task<AdminUsers> GetById(int id);

        public Task<AdminUsers> UpdateAdminUser(AdminUsers User);

        public Task<bool> DeleteAdminUser(int id);

        public Task<AdminUsers> CreateAdminUsers(AdminUsers User);
    }
}
