﻿using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IOrders
    {
        public Task<Orders> GetById(int id);

        public Task<Orders> UpdateOrders(Orders Order);

        public Task<bool> DeleteOrder(int id);

        public Task<Orders> CreateOrder(Orders Order);
    }
}