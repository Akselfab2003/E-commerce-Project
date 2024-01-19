using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface ISupportTickets : IGeneric<SupportTickets>
    {
        public Task<SupportTickets> CreateSupportTicket(SupportTickets entity);
        public Task<SupportTickets> UpdateSupportTicket(SupportTickets entity);
        public Task<bool> DeleteSupportTicket(int id);
        public Task<SupportTickets> GetById(int id);
    }
}
