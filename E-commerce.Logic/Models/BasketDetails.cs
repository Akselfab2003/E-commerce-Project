using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class BasketDetails
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
        public  Products? Products { get; set; }
    
        public  ProductVariants? Variant { get; set;}

       


    }
}