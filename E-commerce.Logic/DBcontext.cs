using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Logic
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options)
        {
                
        }//Testr

        public DbSet<Products> Products { get; set; }


    }
}
