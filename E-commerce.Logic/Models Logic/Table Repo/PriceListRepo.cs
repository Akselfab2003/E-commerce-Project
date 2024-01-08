using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class PriceListRepo : IPriceList
    {
        public Task<PriceList> CreateOrder(PriceList PriceList)
        {
            
        }

        public Task<bool> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PriceList> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PriceList> UpdatePriceList(PriceList PriceList)
        {
            throw new NotImplementedException();
        }
    }
}
