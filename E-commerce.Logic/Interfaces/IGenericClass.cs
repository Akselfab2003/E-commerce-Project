using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IGenericClass<T>
    {
        public  Task<T> Create(T entity);
    }
}
