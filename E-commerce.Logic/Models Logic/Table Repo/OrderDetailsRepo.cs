﻿using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class OrderDetailsRepo : GenericRepo<OrderDetails>, IOrderDetails
    {
        DBcontext context;
        public OrderDetailsRepo(DBcontext c) : base(c) 
        { 
            context = c;
        }

        public async Task<OrderDetails> GetById(int id)
        {
            return await context.OrderDetails.FirstOrDefaultAsync(orderDetails => orderDetails.Id == id);
        }

        public async Task<OrderDetails> UpdateOrderDetails(OrderDetails orderDetails)
        {
            context.Update(orderDetails);
            await context.SaveChangesAsync();
            return orderDetails;
        }


        public async Task<OrderDetails> CreateOrderDetails(OrderDetails orderDetails)
        {
            context.OrderDetails.Add(orderDetails);
            await context.SaveChangesAsync();
            return orderDetails;
        }

        public async Task<bool> DeleteOrderDetails(int id)
        {
            try
            {
                OrderDetails orderDetails = await GetById(id);
                context.OrderDetails.Remove(orderDetails);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
