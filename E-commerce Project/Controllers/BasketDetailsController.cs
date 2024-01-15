using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketDetailsController : Controller
    {
        IBasketDetails context;
        public BasketDetailsController(IDataCollection c) { context = c.BasketDetails; } // Dependency Injection - DI

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDetails>> GetBasketDetailsById(int id)
        {
            var basketDetails = await context.GetById(id);

            if (basketDetails == null)
            {
                return NotFound();
            }

            return basketDetails;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasketDetails(int id, BasketDetails basketDetails)
        {
            if (id != basketDetails.Id)
            {
                return BadRequest();
            }

            try
            {
                await context.UpdateBasketDetails(basketDetails);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BasketDetails>> PostBasketDetails(BasketDetails basketDetails)
        {
            await context.CreateBasketDetails(basketDetails);
            return CreatedAtAction("GetBasketDetails", new { id = basketDetails.Id }, basketDetails);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketDetails(BasketDetails entity)
        {
            var basketDetails = await context.GetById(entity.Id);
            if (basketDetails == null)
            {
                return NotFound();
            }

            context.DeleteBasketDetails(basketDetails);

            return NoContent();
        }
    }
}
