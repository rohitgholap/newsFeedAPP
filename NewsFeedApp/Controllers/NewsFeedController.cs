using Microsoft.AspNetCore.Mvc;
using NewsFeedApp.Models;
using NewsFeedApp.Services;

namespace NewsFeedApp.Controllers
{    
    [ApiController]    
    public class NewsFeedController : ControllerBase
    {
        private readonly INewsFeedServices services;
        private readonly ILogger logger;
        public NewsFeedController(INewsFeedServices services, ILogger logger)
        {
            this.services = services;
            this.logger = logger;
        }

        [Route("api/NewsFeed")]
        [HttpGet]        
        public async Task<IActionResult> GetAllNewsFeeds() 
        {
            try
            {
                logger.LogInformation("NewsFeedApp : Get All Articles");
                var articles = await services.GetArticles();
                return Ok(articles);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("api/NewsFeed/{id}")]
        [HttpGet]        
        public async Task<IActionResult> GetNewsArticle(long id)
        {            
            try
            {
                if (id == 0)
                {
                    return StatusCode(400, "please provide valid article link/id");
                }
                logger.LogInformation($"NewsFeedApp : Get Article id - {id}");
                var article = await services.GetArticle(id);
                return Ok(article);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("api/NewsFeed/DeleteArticle/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(long id)
        {
            try
            {
                if (id > 0)
                {
                    logger.LogInformation($"NewsFeedApp : Delete Article id - {id}");
                    await services.DeleteArticle(id);
                    return Ok("article Deleted Successfully...");
                }
                else
                    return BadRequest("please provide a valid article id");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("api/NewsFeed/UpdateArticle")]
        [HttpPut]
        public async Task<IActionResult> UpdateArticle(Article article)
        {
            try
            {
                if (article != null)
                {
                    logger.LogInformation($"NewsFeedApp : Update Article id - {article.Id}");
                    await services.UpdateArticle(article);
                    return Ok("article updated successfully...");
                }
                else
                    return BadRequest("please provide valid article");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("api/NewsFeed/AddArticle")]
        [HttpPost]
        public async Task<IActionResult> AddArticle(Article article)
        {
            try
            {
                if (article != null)
                {
                    logger.LogInformation($"NewsFeedApp: Add new Article {article}");
                    var newArticle = await services.AddArticle(article);
                    return Ok(newArticle);
                }
                else
                    return BadRequest("bad request, please try again");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
