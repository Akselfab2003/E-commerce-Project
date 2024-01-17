using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class UsersRepo : IUsers
    {
        DBcontext context;
        public UsersRepo(DBcontext c) { context = c; } // Dependency Injection - DI

        public async Task<Users> GetByName(string name)
        {
            return await context.Users.FirstOrDefaultAsync(Users => Users.Username == name);
        }
        public async Task<Users> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(Users => Users.Id == id);
        }

        public async Task<Users> UpdateUser(Users users)
        {
            context.Update(users);
            await context.SaveChangesAsync();
            return users;
        }


        public async Task<bool> CheckLogin(LoginObject loginObject)
        {
            Users UserFromDatabase = await GetByName(loginObject.username);
            
            if(UserFromDatabase == null)
            {
                return false;
            }

            if(ValidatePassword(loginObject.password,UserFromDatabase.Password))
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
                
                if (CompareByteArrays(HashedPasswordAttemptBase64Decoded,PasswordHashFromDatabaseBase64Decoded))
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

        public async Task<bool> DeleteUser(string name)
        {
            try
            {
                Users users = await GetByName(name);
                context.Users.Remove(users);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }


        public async Task<Users> CreateUser(Users users)
        {
            context.Users.Add(users);
            await context.SaveChangesAsync();
            return users;
        }
    }
}
