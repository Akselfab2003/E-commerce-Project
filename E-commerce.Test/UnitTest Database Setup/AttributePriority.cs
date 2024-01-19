using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Test
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AttributePriority : Attribute
    {
        public AttributePriority(int _priority) 
        {
            Priority = _priority;
        }

        public int Priority { get; set; }
    }
}
