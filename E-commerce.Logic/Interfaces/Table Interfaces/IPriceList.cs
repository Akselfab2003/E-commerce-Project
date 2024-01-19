using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IPriceList : IGeneric<PriceList>
    {
        public Task<PriceList> GetById(int id);

        public Task<PriceList> UpdatePriceList(PriceList PriceList);

        public Task<bool> DeleteOrder(int id);

        public Task<PriceList> CreateOrder(PriceList PriceList);
    }
}
