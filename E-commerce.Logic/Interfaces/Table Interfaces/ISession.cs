using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface Isession : IGeneric<Session>
    {
        public Task<Session> GetById(string SessId);
        public Task<Session>UserLogin(LoginObject loginObject);
        public Task<Session> AdminLogin(LoginObject loginObject);
        public Task<List<Session>> GetAllSessions();
    }
}
