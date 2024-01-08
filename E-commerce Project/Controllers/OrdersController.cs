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
        public OrdersController(IOrders _context)
        {
            Context = _context;
        }

        [HttpPost(Name = "CreateOrder")]
        public HttpStatusCode CreateOrder(Orders order)
        {
            try
            {
                Context.CreateOrder(order);
            }
            catch 
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
    }
}
