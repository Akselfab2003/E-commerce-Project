﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public string sessid { get; set; }

        //public List<OrderDetails> OrderLines { get; set; }

      
    }
}
