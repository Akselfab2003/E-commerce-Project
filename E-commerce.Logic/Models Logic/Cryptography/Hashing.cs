using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Cryptography
{
    public class Hashing : IHashing
    {
        private readonly byte[] SALT;

        public Hashing(IConfiguration configuration)
        {
            SALT = Encoding.UTF8.GetBytes(configuration.GetSection("SALT").Value);
        }

       
        public bool ValidatePassword(string PasswordAttempt,string PasswordHashFromDatabase) 
        {
            try
            {
                string HashedPasswordAttempt = HashPassword(PasswordAttempt);
                
                if (string.Equals(HashedPasswordAttempt, PasswordHashFromDatabase))
                {
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public string CreateNewPasswordHash(string Password)
        {
            return HashPassword(Password);
        }


        private string HashPassword(string Password)
        {
            byte[] PasswordBytes = Encoding.UTF8.GetBytes(Password);

            byte[] SaltedPasswordBytes = AddSalt(PasswordBytes);

            string HASH = ComputeHash(SaltedPasswordBytes);
            
            return HASH;
        }

     
        private byte[] AddSalt(byte[] PasswordBytes)
        {
            byte[] SaltedPasswordBytes = PasswordBytes.Concat(SALT).ToArray();

            return SaltedPasswordBytes;
        }

        private string ComputeHash(byte[] bytes)
        {
            byte[] HashedBytes = SHA256.HashData(bytes);
            string HashedString = Convert.ToBase64String(HashedBytes);
            return HashedString;
        }
    }
}
