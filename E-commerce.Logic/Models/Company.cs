using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string cvr {  get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public List<Users>? Users { get; set; } = null;

    }
}
