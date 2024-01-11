using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
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
        private readonly IUsers users;
        private readonly ITags tags;
        private readonly ICategories categories;



        private readonly IProducts products;
        private readonly IImages images;
        private readonly IProductVariants productVariants;
        public DataCollection(DBcontext Context) 
        {
            orders = new ordersRepo(Context);
            users = new UsersRepo(Context);
            tags = new TagsRepo(Context);
            categories = new CategoriesRepo(Context);
            products = new productsRepo(Context);
            images = new ImagesRepo(Context);
            productVariants = new productVariantsRepo(Context);
        }


        public IOrders Orders
        {
            get { return orders; }
        }
        public IUsers Users
        {
            get { return users; }
        }

        public ITags Tags
        {
            get { return tags; }
        }

        public ICategories Categories
        {
            get { return categories; }
        }

        public IProducts Products
        {
            get { return products; }
        }
        public IImages Images
        {
            get { return images; }
        }
        public IProductVariants ProductVariants
        {
            get { return productVariants; }
        }
    }
}
