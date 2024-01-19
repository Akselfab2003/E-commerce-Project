using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class PriceList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual Products ParentProductId {  get; set; } 
        public int Price {  get; set; }

    }
}
