using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Net;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImages images;
        private readonly IDataCollection collection;
        public ImageController(IDataCollection _context)
        {
            images = _context.Images;
            collection = _context;
        }
        #region POST Requests
        [HttpPost]
        public async Task<HttpStatusCode> PostImage(Images image)
        {
            try
            {

                await collection.Images.Create(image);
            }
            catch (Exception ex)
            {

                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        #endregion

        #region GET Requests
        [HttpGet("GetAllImages")]
        public async Task<List<Images>> GetAllImages()
        {
            // var cookies = Request.Cookies;
            try
            {
                List<Images> image = await images.GetAllImages();

                return image;
            }
            catch (Exception ex)
            {
                return new List<Images>();

            }

        }
        #endregion

        #region PUT Requests
        [HttpPut("{id}")]
        public async Task<HttpStatusCode> PutImage(int id,Images image)
        {
            if (id != image.Id)
            {
                return HttpStatusCode.BadRequest;
            }
            try
            {
                await images.UpdateImage(image);
            }
            catch (DbUpdateConcurrencyException)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }
        #endregion

        #region DELETE Requests
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteImages(int id)
        {
            var image = await images.GetById(id);
            if (image == null)
            {
                return HttpStatusCode.BadRequest;
            }

            await collection.Images.DeleteImage(image);

            return HttpStatusCode.NoContent;
        }
        #endregion
    }
}
