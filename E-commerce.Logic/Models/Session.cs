using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class Session
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public string SessId { get; set; }
        //[Required]
        //public Users user { get; set; }

    }
}
