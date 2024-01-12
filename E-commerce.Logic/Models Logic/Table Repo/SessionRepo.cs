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
            Users user = await context.Users.FirstOrDefaultAsync(users => users.Username == loginObject.username && users.Password == loginObject.password);
            Session session = new Session();
            session.user = user;
            session.SessId = Guid.NewGuid().ToString();
            session.Created = DateTime.Now;
            await CreateSession(session);
            return session;
        }

        public async Task<bool> DeleteSession(string id)
        {
            try
            {
                Session session = await GetById(id);
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
            return await context.Sessions.FirstOrDefaultAsync(c => c.SessId == SessID);
        }

        public async Task<Session> UpdateSession(Session session)
        {
            context.Sessions.Update(session);
            await context.SaveChangesAsync();
            return session;
        }
    }
}
