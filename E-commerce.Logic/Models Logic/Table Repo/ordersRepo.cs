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
    public class ordersRepo :IOrders
    {
        private readonly DBcontext context;
        private readonly Isession dataCollection;
        public ordersRepo(DBcontext c) { context = c; dataCollection = new SessionRepo(context); }

        public async Task<Orders> CreateOrder(Orders Order)
        {
            try
            {
                await context.Orders.AddAsync(Order);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            
            
            return Order;

        }

        public async Task<bool> DeleteOrder(int id)
        {
            //var order = GetById(id);
            //if(order != null)
            //{
            //    context.Remove(order);
            //    await context.SaveChangesAsync();
            //    return true;
            //}
            //return false;

            try
            {
                Orders order = await GetById(id);
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<Orders> GetById(int id)
        {
            return await context.Orders.FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<List<Orders>> GetBysessId(string sessid)
        {
            Session session = await dataCollection.GetById(sessid);
            Users usr = session.user;
            List < Orders>userOrders = await context.Orders.Where(order => order.Users == usr).ToListAsync();
            return userOrders;
        }

        public async Task<Orders> UpdateOrders(Orders Order)
        {
            context.Update(Order);
            await context.SaveChangesAsync();
            return Order;
        }

       
    }
}
