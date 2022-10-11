using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.Services;
using BlogApp.Api.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("registr"), AllowAnonymous]
        public async Task<IActionResult> RegistrAsync([FromForm] UserCreateViewModel createViewModel)
        {
            var res = await _accountService.RegistrAsync(createViewModel);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync([FromForm] UserLoginViewModel viewModel)
            => Ok( new { Token = await _accountService.LogInAsync(viewModel)});

        [HttpPost("verifyemail")]
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerify email)
            => Ok(await _accountService.VerifyEmail(email));

        [HttpPost("sendcode")]
        public async Task<IActionResult> SendToEmail([FromBody] SendToEmail email)
        {
            await _accountService.SendCodeAsync(email);
            return Ok();
        }
    }
}
