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

        public DbSet<AdminUsers> AdminUsers { get; set; }
        public DbSet<Company> Company { get; set; }
<<<<<<< HEAD
        public DbSet<DiscountCodes> DiscountCodes { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
=======
>>>>>>> develop
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<PriceList> PriceList { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductVariants> ProductVariants { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
<<<<<<< HEAD
        public DbSet<SupportTickets> SuportTickets { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Users> Users { get; set; }
=======
        public DbSet<Users> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Basket> Basket {  get; set; } 
        public DbSet<BasketDetails> BasketDetails { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<PriceListEntity> priceListEntities { get; set; }

        
         
>>>>>>> develop

    }
}
