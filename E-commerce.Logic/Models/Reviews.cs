using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Users UserId { get; set; }
        public string ReviewContent { get; set; } = string.Empty;
    }
}
