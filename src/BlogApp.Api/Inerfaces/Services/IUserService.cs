using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Entities;
using BlogApp.Api.ViewModels.Users;
using System.Linq.Expressions;

namespace BlogApp.Api.Inerfaces.Services
{
    public interface IUserService
    {
        Task<UserViewModel> UpdateAsync(long id, UserCreateViewModel viewModel);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<UserViewModel> GetAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>>? expression = null);
    }
}
