using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class Orders
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public Users Users { get; set; }

        //public List<OrderDetails> OrderLines { get; set; }

      
    }
}
