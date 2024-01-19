using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface IBasketDetails : IGeneric<BasketDetails>
    {
        public Task<BasketDetails> GetById(int id);

        public Task<BasketDetails> UpdateBasketDetails(BasketDetails basketDetails);

        public Task<bool> DeleteBasketDetails(BasketDetails basketDetails);

        public Task<BasketDetails> CreateBasketDetails(BasketDetails basketDetails);
    }
}
