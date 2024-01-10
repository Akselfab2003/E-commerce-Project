using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models_Logic.Table_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic
{
    public class DataCollection : IDataCollection
    {
        private readonly IOrders orders;
        public DataCollection(DBcontext Context) 
        {
            orders = new ordersRepo(Context);
        }


        public IOrders Orders
        {
            get { return orders; }
        }

    }
}
