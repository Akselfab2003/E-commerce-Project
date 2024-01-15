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
    public class BasketDetailsRepo : IBasketDetails
    {
        DBcontext context;
        public BasketDetailsRepo(DBcontext c) { context = c; } // Dependency Injection - DI

        public async Task<BasketDetails> CreateBasketDetails(BasketDetails basketDetails)
        {
            context.BasketDetails.Add(basketDetails);
            await context.SaveChangesAsync();
            return basketDetails;
        }

        public async Task<bool> DeleteBasketDetails(BasketDetails entity)
        {
            try
            {
                BasketDetails basketDetails = await GetById(entity.Id);
                context.BasketDetails.Remove(basketDetails);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<BasketDetails> GetById(int id)
        {
            return await context.BasketDetails.FirstOrDefaultAsync(basketDetails => basketDetails.Id == id);
        }

        public async Task<BasketDetails> UpdateBasketDetails(BasketDetails basketDetails)
        {
            context.Update(basketDetails);
            await context.SaveChangesAsync();
            return basketDetails;
        }
    }
}
