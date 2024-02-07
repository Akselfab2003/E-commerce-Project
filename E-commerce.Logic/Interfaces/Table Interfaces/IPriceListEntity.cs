using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces.Table_Interfaces
{
    public interface IPriceListEntity : IGeneric<PriceListEntity>
    {
        public Task<PriceListEntity> GetById(int id);
    }
}
