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

        public OrdersController(IDataCollection c) { Context = c.Orders;
            IUserContext = c.Users;
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
        public async Task<HttpStatusCode> CreateOrder(int userid,Orders order)
        {
            try
            {
                var test = order;
                test.Users = await IUserContext.GetById(userid);
             await Context.CreateOrder(test);
            }
            catch 
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
    }
}
