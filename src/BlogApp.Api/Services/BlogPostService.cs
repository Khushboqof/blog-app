using BlogApp.Api.Commons.Exceptions;
using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Entities;
using BlogApp.Api.Inerfaces.Repositories;
using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Blogs;
using BlogApp.Api.ViewModels.Users;
using System.Linq.Expressions;
using System.Net;

namespace BlogApp.Api.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogAppRepository _blogAppRepository;
        private readonly IUserRepositroy _userRepository;
        private readonly IFileService _fileservice;

        public BlogPostService(IBlogAppRepository blogAppRepository,
            IUserRepositroy userRepositroy,
            IFileService fileservice)
        {
            _blogAppRepository = blogAppRepository;
            _userRepository = userRepositroy;
            _fileservice = fileservice;
        }

        public async Task<bool> CreateAsync(BlogCreateViewModel viewModel)
        {
            var blogPost = (BlogPost)viewModel;


            if (blogPost.ImagePath is not null)
                    blogPost.ImagePath = await _fileservice.SaveImageAsync(viewModel.Image);

            if (HttpContextHelper.UserId != null)
                blogPost.UserId = (long)HttpContextHelper.UserId;

            blogPost.CreatedAt = DateTime.UtcNow;

            await _blogAppRepository.CreateAsync(blogPost);
            await _blogAppRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Expression<Func<BlogPost, bool>> expression)
        {
            var post = await _blogAppRepository.GetAsync(expression);

            if (post is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "No such blog post exists");

            await _blogAppRepository.DeleteAsync(expression);
            await _blogAppRepository.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<BlogViewModel>> GetAllAsync(PaginationParams @params, Expression<Func<BlogPost, bool>>? expression = null)
        {
            var posts = _blogAppRepository.GetAll(expression)
                .ToPaged(@params).ToList();

            var blogViews = new List<BlogViewModel>();

            foreach (var post in posts)
            {
                var user = await _userRepository.GetAsync(p => p.Id == post.UserId);

                var blogView = new BlogViewModel()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    Username = (user.FirstName + " " + user.LastName + "," + user.Id),
                    ImageUrl = post.ImagePath,
                    ViewCount = post.ViewCount,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt
                };

                blogViews.Add(blogView);
            }
                
            return blogViews;
        }

        public async Task<BlogViewModel> GetAsync(Expression<Func<BlogPost, bool>> expression)
        {
            var post = await _blogAppRepository.GetAsync(expression);

            if (post is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "Blog not found");
            else
            {
                var user = await _userRepository.GetAsync(p => p.Id == post.UserId);

                var blogView = new BlogViewModel()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    Username = ((user.FirstName + " " + user.LastName)),
                    ViewCount = post.ViewCount,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt
                };

                post.ViewCount++;
                await _blogAppRepository.UpdateAsync(post);
                await _blogAppRepository.SaveAsync();

                return blogView;

            }

        }

        public async Task<BlogPost> UpdateAsync(long id, BlogCreateViewModel viewModel)
        {
            var post = await _blogAppRepository.GetAsync(o => o.Id == id);

            if (post is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "Post not found");

            if (viewModel.Image is not null)
                post.ImagePath = await _fileservice.SaveImageAsync(viewModel.Image);

            post.Title = viewModel.Title;
            post.Description = viewModel.Description;
            post.UpdatedAt = DateTime.UtcNow;

            var UpdatePost = await _blogAppRepository.UpdateAsync(post);
            await _blogAppRepository.SaveAsync();

            return post;
        }
    }
}
