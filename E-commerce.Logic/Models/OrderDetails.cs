using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        public Products ProductId  { get; set; }

        public double price { get; set; }

        public int quantity { get; set; }

        public double total { get; set; }


    }
}
