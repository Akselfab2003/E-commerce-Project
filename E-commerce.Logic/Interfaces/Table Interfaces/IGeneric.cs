using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IGeneric<T>
    {
        /// <summary>
        ///  Adds a given Entity to the database and returns it
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>created entity </returns>
        public Task<T> Create(T entity);
        /// <summary>
        ///  Updates a given Entity and saves it to database and returns it
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>updated entity</returns>
        public Task<T> Update(T entity);
        /// <summary>
        ///  Deletes an Entity from the database and returns it
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Delete entity</returns>
        public Task<T> Delete(T entity);
    }
}
