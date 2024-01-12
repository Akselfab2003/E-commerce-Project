﻿using E_commerce.Logic;
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
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUser(string name)
        {
            var user = await _users.GetByName(name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginObject loginObject)
        {
            var user = await _users.Login(loginObject.username,loginObject.password);

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
        public async Task<IActionResult> PostSession(Users users)
        {
            Session session1 = new Session();
            try
            {
                session1.user = users;
                session1.SessId = Guid.NewGuid().ToString();
                session1.Created = DateTime.Now;
                await _session.CreateSession(session1);
            }
            catch
            {
                return BadRequest();
            }

            return new ObjectResult(session1) { StatusCode = StatusCodes.Status201Created };
        }
        [HttpPut("id")]
        public async Task<IActionResult> PutSession(string name,Session session)
        {
            var availability = session.Created.AddHours(2);
            if (availability < DateTime.Now)
            {
                session.user = await _users.GetByName(name);
            }
            else
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