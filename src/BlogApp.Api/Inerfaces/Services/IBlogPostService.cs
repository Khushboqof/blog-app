using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Entities;
using BlogApp.Api.ViewModels.Blogs;
using System.Linq.Expressions;

namespace BlogApp.Api.Inerfaces.Services
{
    public interface IBlogPostService
    {
        Task<bool> CreateAsync(BlogCreateViewModel viewModel);
        Task<BlogPost> UpdateAsync(long id, BlogCreateViewModel viewModel);
        Task<BlogViewModel> GetAsync(Expression<Func<BlogPost, bool>> expression);
        Task<IEnumerable<BlogViewModel>> GetAllAsync(PaginationParams @params, Expression<Func<BlogPost, bool>>? expression = null);
        Task<bool> DeleteAsync(Expression<Func<BlogPost, bool>> expression);

    }
}