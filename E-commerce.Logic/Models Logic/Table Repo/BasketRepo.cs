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
    public class BasketRepo : GenericRepo<Basket>, IBasket
    {
        private readonly DBcontext context;
        private readonly Isession dataCollection;
        public BasketRepo(DBcontext c) : base(c) 
        { 
            context = c;
        }

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
            return await context.Basket
                .Include(ele => ele.Session)
                .Include(ele => ele.BasketDetails)
                .ThenInclude(ele => ele.Products)
                .ThenInclude(ele => ele.ProductVariants)
                .Include(ele => ele.BasketDetails)
                .ThenInclude(ele => ele.Products)
                .ThenInclude(ele => ele.Images).FirstOrDefaultAsync(Basket => Basket.Id == id);
        }

        public async Task<Basket> GetBySessId(Session sessId)
        {
            Basket userBasket = await context.Basket
                .Include(ele => ele.Session)
                .Include(ele => ele.BasketDetails)
                .ThenInclude(ele => ele.Products)
                .ThenInclude(ele => ele.ProductVariants)
                .Include(ele => ele.BasketDetails)
                .ThenInclude(ele => ele.Products)
                .ThenInclude(ele => ele.Images)
                .FirstOrDefaultAsync(basket => basket.Session == sessId);

            return userBasket;
                
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            context.Update(basket);
            await context.SaveChangesAsync();
            return basket;
        }
    }
}
