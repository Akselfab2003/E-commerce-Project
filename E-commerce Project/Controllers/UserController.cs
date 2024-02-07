using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
        private readonly IAdminUsers _adminUsers;
        private readonly IDataCollection collection;
        public UserController(IDataCollection _context)
        {
            _users = _context.Users;
            _session = _context.Session;
            _adminUsers = _context.AdminUsers;
            collection = _context;
        }

        #region GET Requests
        [HttpGet("GetListOfUsers")]
        public async Task<List<Users?>> GetListOfUsers()
        {
            try
            {
                return await _users.GetListOfUsers();
         
            }
            catch
            {
                return new List<Users>();
            }

        }
        [HttpGet("createEmptySession")]
        public async Task<Session> PostEmptySession()
        {
            Session session1 = new Session();
            try
            {              
                await _session.Create(session1);
                Basket basket = new Basket();
                basket.Session = session1;
                await collection.Basket.Create(basket);
            }
            catch (Exception ex)
            {
                return null;
            }

            return session1; 
        }

        [HttpGet("SessionId{sessionId}")]
        public async Task<Session> GetSession(string sessionId)
        {
            try
            {
                Session session =await _session.GetById(sessionId);
                return session;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new Session();
            }
        }

        [HttpGet("ValidateSession/{sessionId}")]
        public async Task<Boolean> ValidateSession(string sessionId)
        {
            try
            {
                Session session = await _session.GetById(sessionId);
                if (session != null && session.user != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        [Tags(new string[] { "Admin" })]
        [HttpGet("ValidateSessionAdmin/{sessionId}")]
        public async Task<Boolean> ValidateAdminSession(string sessionId)
        {
            try
            {
                Session session = await _session.GetById(sessionId);
                if (session != null)
                {

                    if (session != null && session.admin != null)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        #endregion

        #region   POST Requests
        [HttpPost("createUser")]
        public async Task<HttpStatusCode> PostUser(Users users)
        {
            try
            {
                    users.Password = collection.Cryptography.CreateNewPasswordHash(users.Password);

                    await _users.Create(users);
            }
            catch(Exception ex)
            {
             
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        [Tags(new string[] { "Admin" })]
        [HttpPost("createAdmin")]
        public async Task<HttpStatusCode> PostAdmin(AdminUsers adminUsers)
        {
            try
            {
                adminUsers.Password = collection.Cryptography.CreateNewPasswordHash(adminUsers.Password);

                await _adminUsers.Create(adminUsers);
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        #endregion

        #region   PUT Requests
        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(Users user)
        {
            try
            {
                Users usr = await collection.Users.GetById(user.Id);
                if (user.Password != (await collection.Users.GetById(user.Id)).Password)
                {

                    usr.Password = collection.Cryptography.CreateNewPasswordHash(user.Password);
                
                }
                usr.Username = user.Username;
                usr.Gender = user.Gender;
                usr.Company = user.Company;
                usr.Email = user.Email;
                await collection.Users.Update(usr);

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

            return HttpStatusCode.BadRequest;
            }

        } 


        [HttpPut("Login")]
        public async Task<HttpStatusCode> PutSession(LoginObject loginObject)
        {
            Session session = await _session.GetById(loginObject.sessionId);
            try
            {
                if (session.Created.AddHours(2) > DateTime.Now)
                {
                    

                    loginObject.password = collection.Cryptography.CreateNewPasswordHash(loginObject.password);

                    bool PasswordCorrect = await _users.CheckLogin(loginObject);

                    if (PasswordCorrect)
                    {
                        session = await _session.UserLogin(loginObject);
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return HttpStatusCode.InternalServerError;
            }
            return HttpStatusCode.OK;
        }
        [Tags(new string[] { "Admin" })]
        [HttpPut("AdminLogin")]
        public async Task<HttpStatusCode> PutAdmin(LoginObject loginObject)
        {
            Session session = await _session.GetById(loginObject.sessionId);

            try
            {
                if (session.Created.AddHours(2) > DateTime.Now)
                {


                    loginObject.password = collection.Cryptography.CreateNewPasswordHash(loginObject.password);

                    bool PasswordCorrect = await _adminUsers.CheckLogin(loginObject);

                    if (PasswordCorrect)
                    {
                        session = await _session.AdminLogin(loginObject);
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.OK;
        }
        #endregion

        #region DELETE Requests
        [HttpPost("DeleteUser")]
        public async Task<HttpStatusCode> DeleteUser(Users user)
        {
            try
            {
                await _users.Delete(user);
                return HttpStatusCode.NoContent;
            }
            catch(Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }
        [Tags(new string[] { "Admin" })]
        [HttpDelete("deleteAdmin/{username}")]
        public async Task<IActionResult> DeleteAdmin(string username)
        {
            var user = await _adminUsers.GetByName(username);
            if (user == null)
            {
                return NotFound();
            }

            await _adminUsers.Delete(user);

            return NoContent();
        }
        #endregion

    }
}