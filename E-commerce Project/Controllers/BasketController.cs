﻿using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasket context;
        private readonly IDataCollection dataCollectioncontext;
        public BasketController(IDataCollection c) 
        { 
            context = c.Basket;
            dataCollectioncontext = c;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasketById(int id)
        {
            var basket = await context.GetById(id);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        [HttpGet("GetBasket/{sessid}")]
        public async Task<ActionResult<Basket>> GetBasketBySessId(string sessid)
        {
            Session session = await dataCollectioncontext.Session.GetById(sessid);
            var basket = await context.GetBySessId(session);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasket(int id, Basket basket)
        {
            if (id != basket.Id)
            {
                return BadRequest();
            }

            try
            {
                await context.UpdateBasket(basket);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> PostBasket(Basket basket)
        {
            await context.CreateBasket(basket);
            return CreatedAtAction("GetBasket", new { id = basket.Id }, basket);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(Basket entity)
        {
            var basket = await context.GetById(entity.Id);
            if (basket == null)
            {
                return NotFound();
            }

            context.DeleteBasket(basket);

            return NoContent();
        }
    }
}
