using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public virtual Session Session { get; set; }
        public virtual List<BasketDetails> BasketDetails { get; set; }
    }
}
