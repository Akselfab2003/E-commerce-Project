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
    public class FilterController : ControllerBase
    {
        private readonly ITags DataCollection;
        private readonly ICategories DataCollection_Categories;

        public FilterController(IDataCollection collection)
        {
            DataCollection = collection.Tags;
            DataCollection_Categories = collection.Categories;
        }

        #region GET Requests

        [HttpGet("Categories")]
        public async Task<List<Categories>> GetAllCategories()
        {
            List<Categories> Categories = new List<Categories>();
            try
            {
                Categories = await DataCollection_Categories.GetAllUniqueCategories();
            }
            catch
            {
                return Categories;
            }

            return Categories;
        }

        #endregion

        #region Post Requests

        [HttpPost("Categories")]
        public async Task<ActionResult<Categories>> CreateCategories(Categories categories)
        {
            try
            {
                await DataCollection_Categories.CreateCategories(categories);
            }
            catch (Exception ex)
            { 
            }

            return CreatedAtAction("GetCategories", new { id = categories.Id }, categories);
        }

        #endregion

        #region Put Requests

        [HttpPut("{id}")]
        public async Task<ActionResult<Categories>> UpdateCategories(int id,Categories categories)
        {
            if (id != categories.Id)
            {
                return BadRequest();
            }

            try
            {

                await DataCollection_Categories.UpdateCategories(categories);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        #endregion

        #region Put Requests

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            var categorie = await DataCollection_Categories.GetById(id);
            if (categorie == null)
            {
                return NotFound();
            }

            await DataCollection_Categories.DeleteCategories(categorie);

            return NoContent();
        }

        #endregion
    }
}
