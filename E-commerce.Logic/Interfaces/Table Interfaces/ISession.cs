using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface Isession
    {
        public Task<Session> GetById(string SessID);

        public Task<Session> UpdateSession(Session session);

        public Task<bool> DeleteSession(string id);

        public Task<Session> CreateSession(Session session);
        public Task<Session>Login(LoginObject loginObject);
    }
}
