using BlogApp.Api.Data;
using BlogApp.Api.Entities;
using BlogApp.Api.Inerfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.Api.Repositrories
{
    public class UserRepositrory : IUserRepositroy
    {
        protected readonly AppDbContext appDbContext;
        protected readonly DbSet<User> _dbSet;
        protected readonly DbSet<BlogPost> _dbSetBlog;


        public UserRepositrory(AppDbContext appDb)
        {
            appDbContext = appDb;
            _dbSet = appDbContext.Set<User>();
        }

        public async Task<User> CreateAsync(User user)
        {
            var userRes = await _dbSet.AddAsync(user);

            return userRes.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _dbSet.FirstOrDefaultAsync(expression);

            if (user is null)
                return false;

            _dbSet.Remove(user);

            return true;
        }

        public IQueryable<User> GetAll(Expression<Func<User, bool>>? expression = null)
        {
            return expression is null ? _dbSet : _dbSet.Where(expression);
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task SaveAsync()
        {
            await appDbContext.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            return _dbSet.Update(user).Entity;
        }
    }
}
