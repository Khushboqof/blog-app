using BlogApp.Api.Commons.Exceptions;
using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Commons.Security;
using BlogApp.Api.Entities;
using BlogApp.Api.Inerfaces.Repositories;
using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Users;
using System.Linq.Expressions;
using System.Net;

namespace BlogApp.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositroy _userRepositroy;
        private readonly IFileService _fileService;
        private readonly IBlogPostService _blogPostService;

        public UserService(IUserRepositroy userRepositroy, IFileService fileService, IBlogPostService blogPostService)
        {
            _userRepositroy = userRepositroy;
            _fileService = fileService;
            _blogPostService = blogPostService;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var result = await _userRepositroy.GetAsync(expression);
            if (HttpContextHelper.UserId == result.Id)
            {
                var user = await _userRepositroy.DeleteAsync(expression);
                await _userRepositroy.SaveAsync();
                return user;
            }
            throw new StatusCodeException(HttpStatusCode.NotFound, "Error");
            
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams? pagination = null, Expression<Func<User, bool>>? expression = null)
        {
            var users = _userRepositroy.GetAll(expression).ToPaged(pagination);

            var userviewModel = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                userviewModel.Add((UserViewModel)user);
            }

            return userviewModel;
        }

        public async Task<UserViewModel> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepositroy.GetAsync(expression);

            var viewUser = (UserViewModel)user;

            if (user == null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found");

            return viewUser;
        }

        public async Task<User> UpdateAsync(long id, UserCreateViewModel viewModel)
        {
            var user = await _userRepositroy.GetAsync(o => o.Id == id);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found");

            if (viewModel.Image is not null)
                user.ImagePath = await _fileService.SaveImageAsync(viewModel.Image);

            var hashResult = PasswordHasher.Hash(viewModel.Password);

            user.Salt = hashResult.Salt;

            user.PasswordHash = hashResult.Hash;

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Username = viewModel.Username;
            user.Email = viewModel.Email;
            user.PasswordHash = viewModel.Password;

            var updateUser = await _userRepositroy.UpdateAsync(user);

            await _userRepositroy.SaveAsync();

            return user;
        }
    }
}
