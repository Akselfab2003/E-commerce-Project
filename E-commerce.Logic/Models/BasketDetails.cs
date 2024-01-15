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
        [Required]
        List<Products> Products { get; set; }

    }
}