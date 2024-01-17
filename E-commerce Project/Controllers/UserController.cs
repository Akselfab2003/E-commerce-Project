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
        private readonly IDataCollection collection;
        public UserController(IDataCollection _context)
        {
            _users = _context.Users;
            _session = _context.Session;
            collection = _context;
        }
        [HttpGet("UserName{name}")]
        public async Task<IActionResult> GetUserGetByName(string name)
        {
            var user = await _users.GetByName(name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpGet("UserId{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _users.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost("createUser")]
        public async Task<HttpStatusCode> PostUser(Users users)
        {
            try
            {
                    users.Password = collection.Cryptography.CreateNewPasswordHash(users.Password);

                    await _users.CreateUser(users);
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        [HttpPut]
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
        [HttpDelete]
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
        [HttpGet("createEmptySession")]
        public async Task<Session> PostEmptySession()
        {
            Session session1 = new Session();
            try
            {
                session1.user = null;
                
                await _session.CreateSession(session1);
                Basket basket = new Basket();
                basket.Session = session1;
                await collection.Basket.CreateBasket(basket);
            }
            catch
            {
                return null;//BadRequest();
            }

            return session1; //new ObjectResult(session1) { StatusCode = StatusCodes.Status201Created };
        }
        [HttpPut("Login")]
        public async Task<IActionResult> PutSession(LoginObject loginObject)
        {
            Session session = await _session.GetById(loginObject.sessionId);
     
            try
            {
                if (session.Created > DateTime.Now)
                {
                    

                    loginObject.password = collection.Cryptography.CreateNewPasswordHash(loginObject.password);

                    bool PasswordCorrect = await _users.CheckLogin(loginObject);

                    if (PasswordCorrect)
                    {
                        session = await _session.Login(loginObject);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return new ObjectResult(session) { StatusCode = StatusCodes.Status201Created };
        }
        [HttpGet("SessionId{sessionId}")]
        public async Task<IActionResult> GetSession(string sessionId)
        {
            try
            {
                var session =await _session.GetById(sessionId);
                return Ok(session);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }
        [HttpGet("ValidateSession/{sessionId}")]
        public async Task<Boolean> ValidateSession(string sessionId)
        {
            try
            {
                Session session = await _session.GetById(sessionId);
                if (session == null || session.user==null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}