using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models
{
    public abstract class GenericRepo<T> 
    {
        DBcontext context;


        public  GenericRepo (DBcontext c)
        {
            context = c;
        }
  

        public async Task<T> Create(T entity) 
        {

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }


        public async Task<T> Update(T entity)
        {

            context.Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }


        public async Task<T> Delete(T entity)
        {

            context.Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        
    }
}
