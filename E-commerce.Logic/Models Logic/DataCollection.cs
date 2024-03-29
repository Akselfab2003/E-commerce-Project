﻿using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using E_commerce.Logic.Models_Logic.Cryptography;
using E_commerce.Logic.Models_Logic.Table_Repo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic
{
    public class DataCollection : IDataCollection
    {
        private readonly IAdminUsers adminUsers;
        private readonly IOrders orders;
        private readonly IOrderDetails orderDetails;

        private readonly IUsers users;
        private readonly Isession session;
        private readonly ICategories categories;
        private readonly IProducts products;
        private readonly IImages images;
        private readonly IProductVariants productVariants;
        private readonly IBasketDetails basketDetails;
        private readonly IPriceList priceList;
        private readonly IBasket basket;
        private readonly ICompany company;
        private readonly IHashing cryptography;
        private readonly IReviews reviews;
        private readonly IPriceListEntity priceListEntity;

        public DataCollection(DBcontext Context,IConfiguration configuration) 
        {
            orders = new ordersRepo(Context);
            session = new SessionRepo(Context);
            users = new UsersRepo(Context);
            categories = new CategoriesRepo(Context);
            products = new productsRepo(Context);
            images = new ImagesRepo(Context);
            productVariants = new productVariantsRepo(Context);
            basketDetails = new BasketDetailsRepo(Context);
            basket = new BasketRepo(Context);
            cryptography = new Hashing(configuration);
            adminUsers = new AdminUsersRepo(Context);
            priceList = new PriceListRepo(Context);
            company = new CompanyRepo(Context);
            reviews = new ReviewsRepo(Context);
            orderDetails = new OrderDetailsRepo(Context);
            priceListEntity = new PriceListEntityRepo(Context);
        }

        public IAdminUsers AdminUsers
        {
            get { return adminUsers; }
        }
        public IOrders Orders
        {
            get { return orders; }
        }
        public IUsers Users
        {
            get { return users; }
        }
        public Isession Session
        {
            get { return session; }
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
        public IBasketDetails BasketDetails 
        { 
            get { return basketDetails; } 
        }
        public IBasket Basket
        {
            get { return basket; }
        }
        public IHashing Cryptography
        {
            get { return cryptography; }
        }
        public IPriceList PriceList
        {
            get { return priceList; }
        }

        public ICompany Company
        {
            get { return company; }
        }

        public IReviews Reviews
        {
            get { return reviews; }
        }
        public IOrderDetails OrderDetails
        {
            get { return orderDetails; }
        }
        public IPriceListEntity PriceListEntity
        {
            get { return priceListEntity; }
        }
    }
}
