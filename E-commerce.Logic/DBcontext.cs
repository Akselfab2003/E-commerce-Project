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
                
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductVariants> ProductVariants { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<SupportTickets> SuportTickets { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<DiscountCodes> DiscountCodes { get; set; }


    }
}
