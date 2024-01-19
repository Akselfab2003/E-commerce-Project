﻿using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IAdminUsers : IGeneric<AdminUsers>
    {
        public Task<AdminUsers> GetByName(string name);

        public Task<AdminUsers> UpdateAdminUser(AdminUsers User);

        public Task<bool> DeleteAdminUser(int id);

        public Task<AdminUsers> CreateAdminUsers(AdminUsers User);
        public Task<bool> CheckLogin(LoginObject loginObject);
    }
}
