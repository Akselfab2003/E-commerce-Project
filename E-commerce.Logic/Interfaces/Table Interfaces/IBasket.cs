using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface IBasket
    {
        public Task<Basket> GetById(int id);

        public Task<Basket> UpdateBasket(Basket basket);

        public Task<bool> DeleteBasket(int id);

        public Task<Basket> CreateBasket(Basket basket);
    }
}
