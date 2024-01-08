using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface IBasketDetails
    {
        public Task<BasketDetails> GetById(int id);

        public Task<BasketDetails> UpdateBasket(BasketDetails basket);

        public Task<bool> DeleteBasket(int id);

        public Task<BasketDetails> CreateBasket(BasketDetails basket);
    }
}
