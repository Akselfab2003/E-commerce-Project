using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class DiscountCodes
    {
        [Key]
        public int Id { get; set; }
        public string SalesCode { get; set; } = string.Empty;

        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; }

        public bool HasBeenUsed { get; set; }
        public string UsesLeft { get; set; } = string.Empty;
        public BasketDetails BasketDetails { get; set; }
    }
}
