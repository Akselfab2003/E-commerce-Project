using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IDiscountCodes
    {
        public Task<DiscountCodes> GetById(int id);


        public Task<DiscountCodes> UpdateDiscountCode(DiscountCodes DiscountCode);

        public Task<bool> DeleteDiscountCode(int id);

        public Task<DiscountCodes> CreateDiscountCode(DiscountCodes DiscountCode);
    }
}
