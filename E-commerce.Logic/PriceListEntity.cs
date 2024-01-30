using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic
{
    public class PriceListEntity
    {
        [Key]
        public int Id { get; set; }

        public double PriceListPrice { get; set; }

        private Products product;

        public virtual Products Product
        {
            get {
                    product.Price = PriceListPrice; 
                    return product;
                }
            
            set {
                    product = value;
                }
        }


    }
}
