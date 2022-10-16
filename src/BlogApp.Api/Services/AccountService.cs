using BlogApp.Api.Commons.Exceptions;
using BlogApp.Api.Entities;
using BlogApp.Api.Inerfaces.Managers;
using BlogApp.Api.Inerfaces.Repositories;
using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Users;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using PasswordHasher = BlogApp.Api.Commons.Security.PasswordHasher;

namespace BlogApp.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepositroy _repositroy;
        private readonly IFileService _fileservice;
        private readonly IAuthManager _authManager;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;

        public AccountService(IUserRepositroy userRepository, IFileService fileService, IAuthManager authManager,
            IMemoryCache cache, IEmailService emailService)
        {
            _repositroy = userRepository;
            _fileservice = fileService;
            _authManager = authManager;
            _cache = cache;
            _emailService = emailService;
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

            if(user.IsEmailConfirmed is false)
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "email did not verified!");
        }

        public async Task<bool> RegistrAsync(UserCreateViewModel viewModel)
        {
            var userk = await _repositroy.GetAsync(o => o.Email == viewModel.Email);
            var userkk = await _repositroy.GetAsync(o => o.Username == viewModel.Username);


            if (userk is null && userkk is null)
            {
                var user = (User)viewModel;


                if (user.ImagePath is not null)
                    user.ImagePath = await _fileservice.SaveImageAsync(viewModel.Image);

                var hashResult = PasswordHasher.Hash(viewModel.Password);

                user.Salt = hashResult.Salt;

                user.PasswordHash = hashResult.Hash;

                var result = await _repositroy.CreateAsync(user);

                await _repositroy.SaveAsync();

                var email = new SendToEmail()
                {
                    Email = viewModel.Email
                };

                await SendCodeAsync(email);

                return true;
            }
            else
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "user already exist!");
        }

        public async Task SendCodeAsync(SendToEmail email)
        {
            int code = new Random().Next(1000, 9999);

            _cache.Set(email.Email, code, TimeSpan.FromMinutes(10));

            var message = new EmailMesage()
            {
                To = email.Email,
                Subject = "Verification code",
                Body = code
            };

            await _emailService.SendAsync(message);
        }

        public async Task<bool> VerifyEmail(EmailVerify emailVerify)
        {
            var entity = await _repositroy.GetAsync(user => user.Email == emailVerify.Email);

            if (entity is null)
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "User not found");

            if (_cache.TryGetValue(emailVerify.Email, out int exceptedCode))
            {
                if (exceptedCode != emailVerify.Code)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Code is wrong");

                entity.IsEmailConfirmed = true;

                await _repositroy.UpdateAsync(entity);

                await _repositroy.SaveAsync();

                return true;
            }
            else
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Code is expired");
        }

        public async Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel password)
        {
            var user = await _repositroy.GetAsync(p => p.Email == password.Email);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "user not found!");

            if (user.IsEmailConfirmed is false)
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "email did not verified!");

            var changedPassword = PasswordHasher.ChangePassword(password.Password, user.Salt);

            user.PasswordHash = changedPassword;

            await _repositroy.UpdateAsync(user);
            await _repositroy.SaveAsync();

            throw new StatusCodeException(HttpStatusCode.OK, message: "true");
        }
    }
}
