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
        public OrdersController(IDataCollection c) { Context = c.Orders; }

        [HttpGet("{sessid}")]
        public async Task<ActionResult<Orders>> GetOrderstBysessID(string sessid)
        {
            var order = await Context.GetBysessId(sessid);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("{id}")]
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
        public async Task<HttpStatusCode> CreateOrder(Orders order)
        {
            try
            {
             await Context.CreateOrder(order);
            }
            catch 
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
    }
}
