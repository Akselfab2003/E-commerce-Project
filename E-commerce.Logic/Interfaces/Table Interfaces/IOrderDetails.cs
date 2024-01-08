using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    internal interface IOrderDetails
    {
        public Task<OrderDetails> GetById(int id);

        public Task<OrderDetails> UpdateOrderDetails(OrderDetails OrderDetails);

        public Task<bool> DeleteOrderDetails(int id);

        public Task<OrderDetails> CreateOrderDetails(OrderDetails OrderDetails);
    }
}
