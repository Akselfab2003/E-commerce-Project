    using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
}
