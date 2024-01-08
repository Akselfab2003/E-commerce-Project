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
    public class UserDetailsRepo : IUserDetails
    {
        DBcontext context;
        public UserDetailsRepo(DBcontext c) { context = c; } // Dependency Injection - DI
        
        public async Task<UserDetails> CreateUserDetails(UserDetails entity)
        {
            context.UserDetails.Add(entity);
            await context.SaveChangesAsync(); // SaveChangesAsync()
            return entity;
        }

        public async Task<bool> DeleteUserDetails(int id)
        {
            try
            {
                UserDetails userDetails = await GetById(id);
                context.UserDetails.Remove(userDetails);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<UserDetails> GetById(int id)
        {
            return await context.UserDetails.FirstOrDefaultAsync(userDetails => userDetails.Id == id);
        }

        public async Task<UserDetails> UpdateUserDetails(UserDetails entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
