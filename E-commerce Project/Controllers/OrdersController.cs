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

        [HttpGet("{sessid}")]
        public async Task<ActionResult<List<Orders>>> GetOrderstBysessID(string sessid)
        {
            var order = await Context.GetBysessId(sessid);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Orders>> GetOrderstById(int id)
        {
            var order = await Context.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost(Name = "CreateOrder")]
        public async Task<HttpStatusCode> CreateOrder(string sessid,Orders order)
        {
            try
            {
                var test = order;
                Session session = (await collection.Session.GetById(sessid));
                test.Session = session;
                test.Users = null;
                if(session.user != null)
                {
                   test.Users = session.user;
                }
              
                foreach (OrderDetails details in test.OrderLines)
                {

                    //details.Product = await collection.Products.GetById(details.Product.Id);
                    if (details.variant != null)
                    {
                        details.variant = await collection.ProductVariants.GetById(details.variant.Id);
                        await collection.OrderDetails.Create(details);

                    }
                    else
                    {
                        details.Product = await collection.Products.GetById(details.Product.Id);

                        await collection.OrderDetails.Create(details);
                    }

                    

                }
                await Context.Create(test);
            }
            catch 
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
    }
}
