﻿using E_commerce.Logic.Interfaces.Table_Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    public interface IDataCollection
    {
        public IOrders Orders { get;}

        public IUsers Users { get;}

        public ITags Tags { get; }
        public ICategories Categories { get; }

    }
}
