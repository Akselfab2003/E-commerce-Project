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
        private readonly IProducts products;
        public DataCollection(DBcontext Context) 
        {
            orders = new ordersRepo(Context);
            products = new productsRepo(Context);
        }


        public IOrders Orders
        {
            get { return orders; }
        }
        public IProducts Products
        {
            get { return products; }
        }

    }
}
