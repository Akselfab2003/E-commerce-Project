using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Cryptography
{
    public interface IHashing
    {
        public bool ValidatePassword(string PasswordAttempt, string PasswordHashFromDatabase);

        public string CreateNewPasswordHash(string Password);
    }
}
