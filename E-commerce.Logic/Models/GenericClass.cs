using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public abstract class GenericClass<T> 
    {
        DBcontext context;


        public  GenericClass (DBcontext c)
        {
            context = c;
        }
  

        public async Task<T> Create(T entity) 
        {

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }



    }
}
