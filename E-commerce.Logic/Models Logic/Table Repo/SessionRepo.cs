using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class SessionRepo : GenericRepo<Session>,Isession
    {
        DBcontext context;
  
        public SessionRepo(DBcontext c) : base(c) 
        {
            context = c; 
        }
        public async Task<Session> UserLogin(LoginObject loginObject)
        {
            Users user = context.Users.Where(usr => usr.Username == loginObject.username).First();
            Session session = await GetById(loginObject.sessionId);
            if (session != null)
            {
                session.user = user;
                session.admin = null;
                session.IsAdmin = false;
                await Update(session);
            }

            return session;

        }
        public async Task<Session> AdminLogin(LoginObject loginObject)
        {
            AdminUsers user = context.AdminUsers.Where(usr => usr.Username == loginObject.username).First();
            Session session = await GetById(loginObject.sessionId);
            if (session != null)
            {
                session.user = null;
                session.admin = user;
                session.IsAdmin = true;
                await Update(session);
            }

            return session;

        }



        public async Task<Session> GetById(string SessID)
        {
            try
            {

            return await context.Sessions.FirstOrDefaultAsync(c => c.SessId == SessID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<Session>> GetAllSessions()
        {
            return await context.Sessions.ToListAsync();
        }

    }
}
