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
    public class PriceListEntityRepo : GenericRepo<PriceListEntity>, IPriceListEntity
    {
        DBcontext context;
        public PriceListEntityRepo(DBcontext c) : base(c)
        {
            context = c;
        }
        public async Task<PriceListEntity> GetById(int id)
        {
            return await context.priceListEntities
                .Include(ele=>ele.Product)
                .Include(ele=>ele.Product)
                .ThenInclude(ele=>ele.ProductCategories)
                .Include(ele=>ele.Product)
                .ThenInclude(ele=>ele.Images)
                .FirstOrDefaultAsync(product => product.Id == id);
        }
    }
}
