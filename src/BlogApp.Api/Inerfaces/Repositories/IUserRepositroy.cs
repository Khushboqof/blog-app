using BlogApp.Api.Entities;
using System.Linq.Expressions;

namespace BlogApp.Api.Inerfaces.Repositories
{
    public interface IUserRepositroy
    {
        IQueryable<User> GetAll(Expression<Func<User, bool>>? expression = null);
        Task<User?> GetAsync(Expression<Func<User, bool>> expression);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        Task SaveAsync();
    }
}
