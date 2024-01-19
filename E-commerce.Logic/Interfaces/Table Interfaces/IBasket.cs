using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface IBasket : IGeneric<Basket>
    {
        public Task<Basket> GetById(int id);
        public Task<Basket> GetBySessId(Session sessId);

        public Task<Basket> UpdateBasket(Basket basket);

        public Task<bool> DeleteBasket(Basket basket);

        public Task<Basket> CreateBasket(Basket basket);
    }
}
