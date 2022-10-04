using BlogApp.Api.Data;
using BlogApp.Api.Entities;
using BlogApp.Api.Inerfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.Api.Repositrories
{
    public class BlogAppRepository : IBlogAppRepository
    {
        protected readonly AppDbContext appDbContext;
        protected readonly DbSet<BlogPost> _dbSet;

        public BlogAppRepository(AppDbContext appDb)
        {
            appDbContext = appDb;
            _dbSet = appDbContext.Set<BlogPost>();
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            var post = await _dbSet.AddAsync(blogPost);

            return post.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<BlogPost, bool>> expression)
        {
            var post = await _dbSet.FirstOrDefaultAsync(expression);

            if (post is null)
                return false;

            _dbSet.Remove(post);

            return true;
        }

        public IQueryable<BlogPost> GetAll(Expression<Func<BlogPost, bool>>? expression = null)
        {
            return expression is null ? _dbSet : _dbSet.Where(expression);
        }

        public Task<BlogPost?> GetAsync(Expression<Func<BlogPost, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task SaveAsync()
        {
            await appDbContext.SaveChangesAsync();
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            return _dbSet.Update(blogPost).Entity;
        }
    }
}
