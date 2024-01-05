﻿using System;
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
        public string companyName { get; set; }
        public int cvr {  get; set; }
        public string email { get; set; }

    }
}
