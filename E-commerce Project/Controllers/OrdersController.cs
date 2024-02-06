using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrders Context;
        private readonly IUsers IUserContext;
        private readonly IDataCollection collection;

        public OrdersController(IDataCollection c) 
        {
            Context = c.Orders;
            IUserContext = c.Users;
            collection = c;
        }

        #region GET Requests
        [HttpGet("{sessid}")]
        public async Task<List<Orders>> GetOrdersBysessID(string sessid)
        {
            try
            {
            var order = await Context.GetBysessId(sessid);

            if (order == null)
            {
                return new List<Orders>();
            }

            return order;

            }
            catch (Exception ex)
            {
                return new List<Orders>();
            }
        }

        [HttpGet("CreateOrder/{sessid}")]
        public async Task<Orders> GetOrderBysessID(string sessid)
        {
            try
            {
                var order = await Context.GetSingleOrderBySessId(sessid);
                if(order == null)
                {
                    return new Orders();
                }
                else
                {
                    return order;
                }

            }
            catch (Exception ex)
            {
                return new Orders();
            }
        }

        [HttpGet("id/{id}")]
        public async Task<Orders> GetOrdersById(int id)
        {
            var order = await Context.GetById(id);

            if (order == null)
            {
                return null;
            }

            return order;
        }
        #endregion

        #region POST Reguests
        [HttpPost(Name = "CreateOrder")]
        public async Task<Session?> CreateOrder(string sessid,Orders order)
        {

            Session session = (await collection.Session.GetById(sessid));
            try
            {
                var test = order;
                test.Session = session;
                test.Users = null;
                if(session.user != null)
                {
                   test.Users = session.user;
                }
              
                foreach (OrderDetails details in test.OrderLines)
                {
                    if (details.variant != null)
                    {
                        details.variant = await collection.ProductVariants.GetById(details.variant.Id);
                        await collection.OrderDetails.Create(details);

                    }
                    else
                    {
                        details.Product = await collection.Products.GetById(details.Product.Id);
                        details.variant = null;
                        await collection.OrderDetails.Create(details);
                    }
                }
                await Context.Create(test);
            }
            catch 
            {
                
                return null ;
            }

            UserController userController = new UserController(collection);
            Session newsession = await userController.PostEmptySession();
            newsession.user = null;
            if(session.user != null)
            {
                newsession.user = session.user;
            }
           
            await collection.Session.Update(session);



            return newsession;
        }
        #endregion
    }
}
