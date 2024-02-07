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
<<<<<<< HEAD
        public Users UserId { get; set; }
        public string ReviewContent { get; set; } = string.Empty;
=======
        public virtual Users UserId { get; set; }
        [Required]
        public Products Products { get; set; }
        public string ReviewContent { get; set; } = string.Empty;
        public string ReviewTitle { get; set; }
        public int ReviewRating { get; set; }
>>>>>>> develop
    }
}
