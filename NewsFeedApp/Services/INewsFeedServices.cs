using NewsFeedApp.Models;
using System.Collections;

namespace NewsFeedApp.Services
{
    public interface INewsFeedServices
    {
        public Task<IEnumerable<Article>> GetArticles();
        public Task<Article> GetArticle(long id);
        public Task DeleteArticle(long id);        
        public Task<Article> AddArticle(Article article);
        public Task UpdateArticle(Article article);         
    }
}
