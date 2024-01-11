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
    public class ImagesController : Controller
    {
        IImages context;
        public ImagesController(IDataCollection c) { context = c.Images; } // Dependency Injection - DI

        [HttpGet("{id}")]
        public async Task<ActionResult<Images>> GetimageById(int id)
        {
            var image = await context.GetById(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Putimage(int id, Images images)
        {
            if (id != images.Id)
            {
                return BadRequest();
            }

            try
            {
                await context.UpdateImage(images);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Images>> Postimage(Images image)
        {
            await context.CreateImage(image);
            return CreatedAtAction("Getimage", new { id = image.Id }, image);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteimage(int id)
        {
            var image = await context.GetById(id);
            if (image == null)
            {
                return NotFound();
            }

            context.DeleteImage(image);

            return NoContent();
        }
    }
}
