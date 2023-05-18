using NewsFeedApp.Models;
using NewsFeedApp.Models.Repository;

namespace NewsFeedApp.Services
{
    public class NewsFeedServices : INewsFeedServices
    {
        private readonly IRepository repo;
        private readonly ICacheService cacheService;
        private readonly ILogger logger;
        public NewsFeedServices(IRepository repo, ICacheService cacheService, ILogger logger)
        {
            this.repo = repo;
            this.cacheService = cacheService;
            this.logger = logger;
        }

        public async Task<Article> AddArticle(Article article)
        {
            Article newArticle = null;
            try
            {
                newArticle = await repo.AddArticle(article);
                cacheService.RemoveData("articles");
            }
            catch (Exception ex)
            {
                logger.LogError($"exception thrown in add article {ex.Message}");
                throw;
            }
            return newArticle;
        }

        public async Task DeleteArticle(long id)
        {
            try
            {
                await repo.DeleteArticle(id);
                cacheService.RemoveData("articles");
            }
            catch (Exception ex)
            {
                logger.LogError($"exception thrown in delete article {ex.Message}");
                throw;
            }
        }

        public async Task<Article> GetArticle(long id)
        {
            Article article = new Article();
            try
            {
                var cacheData = cacheService.GetData<IEnumerable<Article>>("articles");
                if(cacheData!=null)
                {
                    var filteredData = cacheData.Where(x=>x.Id==id).FirstOrDefault();
                    article = filteredData;
                    return article;
                }
                article = await repo.GetArticle(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"exception thrown in get article {ex.Message}");
                throw;
            }
            return article;
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            IEnumerable<Article> articles = new List<Article>();            
            try
            {
                var cacheData = cacheService.GetData<IEnumerable<Article>>("articles");
                if (cacheData != null)
                    return cacheData;                

                articles = await repo.GetAll();
                
                cacheData = articles;
                var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheService.SetData<IEnumerable<Article>>("articles", cacheData, expirationTime);
            }
            catch(Exception ex)
            {
                logger.LogError($"exception thrown in get articles {ex.Message}");
                throw;
            }
            return articles;
        }

        public async Task UpdateArticle(Article article)
        {
            try
            {
                await repo.UpdateArticle(article);
                cacheService.RemoveData("articles");
            }
            catch(Exception ex) 
            {
                logger.LogError($"exception thrown in update article {ex.Message}");
                throw;
            }
        }
    }
}
