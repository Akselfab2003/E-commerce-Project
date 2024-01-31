using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class Orders
    {
        [Key]
        
        public int Id { get; set; }


        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public virtual Users? Users { get; set; }

        public virtual Session? Session { get; set; }

        public virtual List<OrderDetails> OrderLines { get; set; }


        public double Total {  get; set; }

      
    }
}
