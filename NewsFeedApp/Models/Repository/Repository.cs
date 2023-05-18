using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace NewsFeedApp.Models.Repository
{
    public class Repository : IRepository
    {
        private readonly NewsFeedContext context;
        public Repository(NewsFeedContext newsFeedContext)
        {
            this.context = newsFeedContext;
        }

        public async Task<Article> AddArticle(Article obj)
        {
            try
            {
                var newArticle = await context.Articles.AddAsync(obj);
                context.SaveChanges();
                return newArticle.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteArticle(long id)
        {
            try
            {
                var articleToDelete = await context.Articles.FirstOrDefaultAsync(x => x.Id == id);
                if (articleToDelete != null)
                {
                    context.Remove(articleToDelete);
                }
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            try
            {
                return await context.Articles.ToListAsync();
            }
            catch(Exception ex)
            {
                throw;
            }            
        }

        public async Task<Article> GetArticle(long id)
        {
            try
            {

                var article = await context.Articles.FindAsync(id);
                if (article == null)
                    return null;
                return article;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateArticle(Article update)
        {
            try
            {
                context.Articles.Update(update);
                context.SaveChanges();
                
            }
            catch(Exception ex)
            {
                throw;
            }
        }      
    }
}
