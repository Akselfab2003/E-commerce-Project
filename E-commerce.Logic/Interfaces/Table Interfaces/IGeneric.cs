using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IGeneric<T>
    {
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(T entity);
    }
}
