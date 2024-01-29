using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Logic.Models;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ITags DataCollection;
        private readonly IReviews DataCollection_Reviews;
        private readonly IDataCollection DataCollection_controller;

        public ReviewsController(IDataCollection collection)
        {
            DataCollection = collection.Tags;
            DataCollection_Reviews = collection.Reviews;
            DataCollection_controller = collection;
        }

        #region GET Requests
        //GET:
        [HttpGet("Get_Reviews{productid}")]
        public async Task<ActionResult<List<Reviews>>> GetReviewsByProductid(int id)
        {
            List<Reviews> reviews = new List<Reviews>();
            try
            {
                reviews = await DataCollection_Reviews.GetByProductId(id);
            }
            catch (Exception ex)
            {
                return reviews;
            }
            return reviews;
        }
        #endregion

        #region Post Requests
        [HttpPost("CreateReviews")]
        public async Task<ActionResult<Reviews>> createReviews(Reviews reviews, string sessId)
        {
            try
            {
                reviews.UserId = (await DataCollection_controller.Session.GetById(sessId)).user;
                await DataCollection_Reviews.Create(reviews);
            }
            catch
            {

            }
            return CreatedAtAction("GetReviews", new { id = reviews.Id }, reviews);
        }
        #endregion

        #region Delete Request
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await DataCollection_Reviews.GetById(id);
            if(review == null)
            {
                return NotFound();
            }
            await DataCollection_Reviews.Delete(review);

            return NoContent();
        }
        #endregion
    }
}
