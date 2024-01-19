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
    public class DiscountCodesRepo : GenericRepo<DiscountCodes>, IDiscountCodes
    {
        DBcontext Context;
        public DiscountCodesRepo(DBcontext context) : base(context)
        {
            Context = context;
        }
        public async Task<DiscountCodes> CreateDiscountCode(DiscountCodes DiscountCode)
        {

            Context.DiscountCodes.Add(DiscountCode);
            
            await Context.SaveChangesAsync();

            return DiscountCode;
        }

        public async Task<bool> DeleteDiscountCode(int id)
        {
            try
            {
                DiscountCodes discountCode = await GetById(id);

                Context.DiscountCodes.Remove(discountCode);

                await Context.SaveChangesAsync();
            }
            catch 
            {
                return false;
            }
           

            return true;
        }

        public async Task<DiscountCodes> GetById(int id)
        {
           return await Context.DiscountCodes.FirstOrDefaultAsync(c => c.Id == id);
        }

        

        public async Task<DiscountCodes> UpdateDiscountCode(DiscountCodes DiscountCode)
        {
            Context.DiscountCodes.Update(DiscountCode);

            await Context.SaveChangesAsync();

            return DiscountCode;

        }
    }
}
