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



        [HttpGet("Categories")]
        public async Task<Categories?> GetCategoryById(int id)
        {
           
            try
            {
               return await DataCollection_Categories.GetById(id);
            }
            catch
            {
                return null;
            }

            return null;
        }

        #endregion

        #region Post Requests

        [HttpPost("Categories")]
        public async Task<HttpStatusCode> CreateCategories(Categories categories)
        {
            try
            {
                await DataCollection_Categories.CreateCategories(categories);
            }
            catch (Exception ex)
            { 
            }

            return HttpStatusCode.Created;
        }

        #endregion

        #region Put Requests

        [HttpPut("{id}")]
        public async Task<HttpStatusCode> UpdateCategories(int id,Categories categories)
        {
            if (id != categories.Id)
            {
                return HttpStatusCode.BadRequest;
            }

            try
            {

                await DataCollection_Categories.UpdateCategories(categories);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return HttpStatusCode.OK;
        }

        #endregion

        #region Delete Requests

        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteCategories(int id)
        {
            var categorie = await DataCollection_Categories.GetById(id);
            if (categorie == null)
            {
                return HttpStatusCode.NotFound;
            }

            await DataCollection_Categories.DeleteCategories(categorie);

            return HttpStatusCode.NoContent;
        }

        #endregion
    }
}
