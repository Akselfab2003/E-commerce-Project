using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Logic.Models;
using System.Net;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviews DataCollection_Reviews;
        private readonly IDataCollection DataCollection_controller;

        public ReviewsController(IDataCollection collection)
        {
            DataCollection_Reviews = collection.Reviews;
            DataCollection_controller = collection;
        }

        #region GET Requests
        //GET:
        [HttpGet("Get_Reviews/{productid}")]
        public async Task<List<Reviews>> GetReviewsByProductid(int productid)
        {
            List<Reviews> reviews = new List<Reviews>();
            try
            {
                reviews = await DataCollection_Reviews.GetByProductId(productid);
            }
            catch (Exception ex)
            {
                return reviews;
            }
            return reviews;
        }
        #endregion

        #region Post Requests
        [HttpPost("CreateReviews/{sessId}")]
        public async Task<HttpStatusCode> createReviews(Reviews reviews, string sessId)
        {
            try
            {
                reviews.UserId = (await DataCollection_controller.Session.GetById(sessId)).user;
                reviews.Products = (await DataCollection_controller.Products.GetById(reviews.Products.Id));
                await DataCollection_Reviews.Create(reviews);
            }
            catch
            {

            }
            //return CreatedAtAction("GetReviews", new { id = reviews.Id }, reviews);
            return HttpStatusCode.Created;
        }
        #endregion

    
    }
}
