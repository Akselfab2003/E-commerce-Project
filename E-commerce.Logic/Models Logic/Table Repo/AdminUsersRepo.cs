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
    public class AdminUsersRepo : GenericClass<AdminUsers> ,IAdminUsers
    {
        DBcontext context;
        DbSet<AdminUsers> adminUsers;

        public AdminUsersRepo(DBcontext c) :base(c)
        {   this.context = c;
            //adminUsers = c.AdminUsers;
        }

        public async Task<AdminUsers> GetByName(string name)
        {
            return await context.AdminUsers.FirstOrDefaultAsync(adminUsers => adminUsers.Username == name);
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
                AdminUsers adminusers = await context.AdminUsers.FirstOrDefaultAsync(adminUsers => adminUsers.Id == id);
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
        public async Task<bool> CheckLogin(LoginObject loginObject)
        {
            AdminUsers UserFromDatabase = await GetByName(loginObject.username);

            if (UserFromDatabase == null)
            {
                return false;
            }

            if (ValidatePassword(loginObject.password, UserFromDatabase.Password))
            {
                return true;
            }

            return false;

        }
        public bool ValidatePassword(string HashedPasswordAttempt, string PasswordHashFromDatabase)
        {
            try
            {
                byte[] HashedPasswordAttemptBase64Decoded = Convert.FromBase64String(HashedPasswordAttempt);
                byte[] PasswordHashFromDatabaseBase64Decoded = Convert.FromBase64String(PasswordHashFromDatabase);

                if (CompareByteArrays(HashedPasswordAttemptBase64Decoded, PasswordHashFromDatabaseBase64Decoded))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CompareByteArrays(byte[] A, byte[] B)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] != B[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
