using BlogApp.Api.Entities;
using System.Linq.Expressions;

namespace BlogApp.Api.Inerfaces.Repositories
{
    public interface IBlogAppRepository
    {
        IQueryable<BlogPost> GetAll(Expression<Func<BlogPost, bool>>? expression = null);
        Task<BlogPost?> GetAsync(Expression<Func<BlogPost, bool>> expression);
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<BlogPost> UpdateAsync(BlogPost blogPost);
        Task<bool> DeleteAsync(Expression<Func<BlogPost, bool>> expression);
        Task SaveAsync();
    }
}
