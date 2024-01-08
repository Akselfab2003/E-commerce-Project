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
    public class SupportTicketsRepo : ISupportTickets
    {
        DBcontext Context;
        public SupportTicketsRepo(DBcontext context)
        {

            Context = context;

        }
        public async Task<SupportTickets> CreateSupportTicket(SupportTickets entity)
        {
           Context.SuportTickets.Add(entity);

           await Context.SaveChangesAsync();
           
           return entity;

        }

        public async Task<bool> DeleteSupportTicket(int Id)
        {
            try
            {
                SupportTickets SupportTicketsEntity = await GetById(Id);

                Context.SuportTickets.Remove(SupportTicketsEntity);

                await Context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }


            return true;
        }

      

        public async Task<SupportTickets> GetById(int id)
        {
            return await Context.SuportTickets.FirstOrDefaultAsync(t => t.Id == id);
        }

        

        public async Task<SupportTickets> UpdateSupportTicket(SupportTickets entity)
        {
            Context.SuportTickets.Update(entity);

            await Context.SaveChangesAsync();
            return entity;
        }
    }
}
