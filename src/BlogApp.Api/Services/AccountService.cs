using BlogApp.Api.Commons.Exceptions;
using BlogApp.Api.Entities;
using BlogApp.Api.Inerfaces.Managers;
using BlogApp.Api.Inerfaces.Repositories;
using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Users;
using System.Net;
using PasswordHasher = BlogApp.Api.Commons.Security.PasswordHasher;

namespace BlogApp.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepositroy _repositroy;
        private readonly IFileService _fileservice;
        private readonly IAuthManager _authManager;

        public AccountService(IUserRepositroy userRepository, IFileService fileService, IAuthManager authManager)
        {
            _repositroy = userRepository;
            _fileservice = fileService;
            _authManager = authManager;
        }

        public async Task<string> LogInAsync(UserLoginViewModel viewModel)
        {
            var user = await _repositroy.GetAsync(o => o.Email == viewModel.Email);

            if (user is not null)
            {
                if (PasswordHasher.Verify(viewModel.Password, user.Salt, user.PasswordHash))
                {
                    return _authManager.GenerateToken(user);
                }
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "password is wrong");
            }
            throw new StatusCodeException(HttpStatusCode.NotFound, message: "email is wrong");
        }

        public async Task<bool> RegistrAsync(UserCreateViewModel viewModel)
        {
            var userk = await _repositroy.GetAsync(o => o.Email == viewModel.Email);

            if (userk is null)
            {
                var user = (User)viewModel;


                if (user.ImagePath is not null)
                    user.ImagePath = await _fileservice.SaveImageAsync(viewModel.Image);

                var hashResult = PasswordHasher.Hash(viewModel.Password);
                user.Salt = hashResult.Salt;
                user.PasswordHash = hashResult.Hash;
                var result = await _repositroy.CreateAsync(user);
                await _repositroy.SaveAsync();

                return true;
            }
            throw new StatusCodeException(HttpStatusCode.BadRequest, message: "user already exist!");
        }
    }
}
