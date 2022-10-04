using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Users;
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

        [HttpPost("registr")]
        public async Task<IActionResult> RegistrAsync([FromForm] UserCreateViewModel createViewModel)
        {
            var res = await _accountService.RegistrAsync(createViewModel);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync([FromForm] UserLoginViewModel viewModel)
            => Ok( new { Token = await _accountService.LogInAsync(viewModel)});
    }
}
