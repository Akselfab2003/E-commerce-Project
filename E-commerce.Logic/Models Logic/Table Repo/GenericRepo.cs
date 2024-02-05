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
            try
            {

            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                
            }

            return entity;
        }



        public async Task<T> Update(T entity)
        {
            try
            {

            context.Update(entity);
            await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return entity;
        }


        public async Task<T> Delete(T entity)
        {
            try
            {

             context.Remove(entity);
             await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
            
            return entity;
        }

        
    }
}
