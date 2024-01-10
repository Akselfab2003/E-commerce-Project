using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _users;
        private readonly Isession _session;
        public UserController(IDataCollection _context)
        {
            _users = _context.Users;
            _session = _context.Session;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _users.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost(Name = "CreateUser")]
        public async Task<HttpStatusCode> Create(Users users)
        {
            try
            {
                await _users.CreateUser(users);
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id,Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _users.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _users.DeleteUser(user.Id);

            return NoContent();
        }
        [HttpPost(Name = "Test")]
        public async Task<HttpStatusCode> CreateSession(Session session)
        {
            try
            {
                await _session.CreateSession(session);
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        //[HttpPut("id")]
        //public async Task<IActionResult> PutSession(Session session)
        //{
        //    var availability = session.Created.AddHours(2);
        //    if (availability < DateTime.Now)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await _session.UpdateSession(session);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //    }
        //    return NoContent();
        //}
    }
}
