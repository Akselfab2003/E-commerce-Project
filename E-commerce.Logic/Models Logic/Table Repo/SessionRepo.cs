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
    public class SessionRepo : Isession
    {
        DBcontext context;
  
        public SessionRepo(DBcontext c) { context = c; }
        public async Task<Session> CreateSession(Session session)
        {
            context.Sessions.Add(session);
            await context.SaveChangesAsync();
            return session;
        }
        public async Task<Session> Login(LoginObject loginObject)
        {
                Users user = context.Users.Where(usr => usr.Username == loginObject.username).First();
                Session session = await GetById(loginObject.sessionId);
                if(session != null)
                {
                    session.user = user;
                    await UpdateSession(session);
                }
                
                return session;

        }

        public async Task<bool> DeleteSession(string id)
        {
            try
            {
                Session session = await context.Sessions.FirstOrDefaultAsync(sessions=>sessions.SessId==id);
                context.Sessions.Remove(session);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<Session> GetById(string SessID)
        {
            return await context.Sessions.Include(sess=>sess.user).FirstOrDefaultAsync(c => c.SessId == SessID);
        }


        public async Task<List<Session>> GetAllSessions()
        {
            return await context.Sessions.ToListAsync();
        }
        public async Task<Session> UpdateSession(Session session)
        {
            context.Sessions.Update(session);
            await context.SaveChangesAsync();
            return session;
        }
    }
}
