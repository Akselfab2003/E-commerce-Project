using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string SessId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public DateTime Created { get; set; } = DateTime.Now.AddHours(2);
        [AllowNull]
        [ForeignKey("Users")]
        public virtual Users? user { get; set; }
        [AllowNull]
        [ForeignKey("AdminUsers")]
        public virtual AdminUsers? admin { get; set; } = null;
        public bool IsAdmin { get; set; } = false;

    }
}
