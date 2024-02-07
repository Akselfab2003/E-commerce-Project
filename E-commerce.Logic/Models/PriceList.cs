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
<<<<<<< HEAD
        [Required]
        public Products ParentProductId {  get; set; } 
        public int Price {  get; set; }
=======
        public string Name { get; set; }
        public virtual List<PriceListEntity>? PriceListProducts {  get; set; } 

        public virtual List<Company>? Companies { get; set; }

        public virtual List<Users>? Users { get; set; }

>>>>>>> develop

    }
}
