using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models_Logic.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IDataCollection
    {
        public IOrders Orders { get; }
        public IUsers Users { get; }
        public Isession Session { get; }
        public IProducts Products { get; }
        public IImages Images { get; }
        public IProductVariants ProductVariants { get; }
        public ITags Tags { get; }
        public ICategories Categories { get; }
        public IBasketDetails BasketDetails { get; }
        public IBasket Basket {  get; }
        public IHashing Cryptography {  get; }

    }
}
