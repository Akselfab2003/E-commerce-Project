<<<<<<< HEAD
﻿using System;
=======
﻿using Microsoft.EntityFrameworkCore;
using System;
>>>>>>> develop
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
<<<<<<< HEAD
=======
    [Index("Username",IsUnique = true)]
>>>>>>> develop
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
<<<<<<< HEAD
        [Column(TypeName = "Binary")]
        public bool Gender { get; set; }
=======
        public bool Gender { get; set; }
        public virtual Company? Company { get; set; }
>>>>>>> develop
    }
}
