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
    public class PriceListRepo : GenericRepo<PriceList>, IPriceList
    {
        DBcontext context;
        public PriceListRepo(DBcontext c) : base(c)
        {
            context = c;
        } // Dependency Injection - DI

        public async Task<PriceList> CreateOrder(PriceList PriceList)
        {
            context.PriceList.Add(PriceList);
            await context.SaveChangesAsync();
            return PriceList;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                PriceList priceList = await GetById(id);
                context.PriceList.Remove(priceList);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<PriceList> GetById(int id)
        {
            return await context.PriceList.FirstOrDefaultAsync(priceList => priceList.Id == id);
        }

        public async Task<PriceList> UpdatePriceList(PriceList PriceList)
        {
            context.Update(PriceList);
            await context.SaveChangesAsync();
            return PriceList;
        }
    }
}
