using System.Collections;
using System.Collections.Generic;

namespace NewsFeedApp.Models.Repository
{
    public interface IRepository
    {
        public Task<IEnumerable<Article>> GetAll();
        public Task<Article> GetArticle(long id);

        public Task<Article> AddArticle(Article obj);

        public Task UpdateArticle(Article id);

        public Task DeleteArticle(long id);

    }
}
