using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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
        [Required]
        public DateTime Created { get; set; }
        [AllowNull]
        public Users? user { get; set; } 

    }
}
