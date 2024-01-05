using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface ISupportTickets
    {
        public Task<SupportTickets> Create(SupportTickets entity);
        public Task<SupportTickets> Update(SupportTickets entity);
        public Task<bool> Delete(SupportTickets entity);
        public Task<SupportTickets> Get(SupportTickets entity);
        public Task<SupportTickets> GetById(int id);
        public Task<SupportTickets> GetByName(string name);
    }
}
