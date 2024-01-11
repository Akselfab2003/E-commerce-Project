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
    public class TagsController : ControllerBase
    {
        private readonly ITags DataCollection;
        private readonly ICategories DataCollection_Categories;

        public TagsController(IDataCollection collection)
        {
            DataCollection = collection.Tags;
            DataCollection_Categories = collection.Categories;
        }

        [HttpPost(Name = "GetAllTags")]
        public async Task<List<Tags>> GetAllTagsBasedOnIds(List<int> Ids)
        {
            List<Tags> Tags = new List<Tags>();
            try
            {
              Tags =   await DataCollection.GetTagsForListOfIds(Ids);
            }
            catch
            {
                return Tags;
            }

            return Tags;
        }


        [HttpGet]
        public async Task<List<Tags>> GetAllTags()
        {
            List<Tags> Tags = new List<Tags>();
            try
            {
                Tags = await DataCollection.GetAllUniqueTags();
            }
            catch
            {
                return Tags;
            }

            return Tags;
        }


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

    }
}
