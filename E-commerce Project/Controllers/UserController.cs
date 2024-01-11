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
        public async Task<IActionResult> GetUser(string name)
        {
            var user = await _users.GetByName(name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost]
        public async Task<HttpStatusCode> PostUser(Users users)
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
        public async Task<IActionResult> DeleteUser(string name)
        {
            var user = await _users.GetByName(name);
            if (user == null)
            {
                return NotFound();
            }

            await _users.DeleteUser(user.Username);

            return NoContent();
        }
        [HttpPost("Session")]
        public async Task<HttpStatusCode> PostSession(string name,Session session)
        {
            try
            {   Session session1 = new Session();
                session1.user = await _users.GetByName(name);
                session1.SessId = session.SessId;
                session1.Created = session.Created;
                await _session.CreateSession(session1);
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        [HttpPut("id")]
        public async Task<IActionResult> PutSession(Session session)
        {
            var availability = session.Created.AddHours(2);
            if (availability < DateTime.Now)
            {
                return BadRequest();
            }

            try
            {
                await _session.UpdateSession(session);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }
    }
}
