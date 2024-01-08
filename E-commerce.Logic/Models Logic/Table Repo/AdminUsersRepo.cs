using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class AdminUsersRepo : IAdminUsers
    {
        DBcontext context;
        public AdminUsersRepo(DBcontext c) { context = c; }

        public async Task<AdminUsers> GetById(int id)
        {
            return await context.AdminUsers.FirstOrDefaultAsync(adminUsers => adminUsers.Id == id);
        }

        public async Task<AdminUsers> UpdateAdminUser(AdminUsers User)
        {
            context.Update(User);
            await context.SaveChangesAsync();
            return User;
        }

        public async Task<bool> DeleteAdminUser(int id)
        {
            try
            {
                AdminUsers adminusers = await GetById(id);
                context.AdminUsers.Remove(adminusers);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<AdminUsers> CreateAdminUsers(AdminUsers User)
        {
            await context.AdminUsers.AddAsync(User);
            await context.SaveChangesAsync(); // SAveChangesAsync()
            return User;
        }
    }
}
