using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly Isession Context;
        public SessionController(IDataCollection _context)
        {
            //Context = _context.Session;
        }
        //[HttpPost(Name = "CreateSession")]
        //public async Task<HttpStatusCode> CreateSession(Session session)
        //{
        //    try
        //    {
        //        await Context.CreateSession(session);
        //    }
        //    catch
        //    {
        //        return HttpStatusCode.BadRequest;
        //    }

        //    return HttpStatusCode.Created;
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSession(Session session)
        //{
        //    var availability = session.Created.AddHours(2);
        //    if (availability < DateTime.Now)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await Context.UpdateSession(session);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //    }
        //    return NoContent();
        //}
    }
}
