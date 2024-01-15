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
    public class BasketRepo : IBasket
    {
        DBcontext context;
        public BasketRepo(DBcontext c) { context = c; } // Dependency Injection - DI

        public async Task<Basket> CreateBasket(Basket basket)
        {
            context.Basket.Add(basket);
            await context.SaveChangesAsync();
            return basket;
        }

        public async Task<bool> DeleteBasket(Basket entity)
        {
            try
            {
                Basket basket = await GetById(entity.Id);
                context.Basket.Remove(basket);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<Basket> GetById(int id)
        {
            return await context.Basket.Include(basket => basket.BasketDetails).FirstOrDefaultAsync(Basket => Basket.Id == id);
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            context.Update(basket);
            await context.SaveChangesAsync();
            return basket;
        }
    }
}
