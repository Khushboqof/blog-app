using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBlogPostService _blogPostService;

        public UsersController(IUserService userService, IBlogPostService blogPostService)
        {
            _userService = userService;
            _blogPostService = blogPostService;
        }

        [HttpGet("id"), Authorize]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _userService.GetAsync(user => user.Id == HttpContextHelper.UserId));
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteAsync()
            => Ok(await _userService.DeleteAsync(o => o.Id == HttpContextHelper.UserId));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserCreateViewModel userCreateView)
            => Ok(await _userService.UpdateAsync(userCreateView));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _userService.GetAllAsync(@params));

        [HttpGet("{id}/blogposts")]
        public async Task<IActionResult> GetAllBlogPostsAsync(long id, [FromQuery] PaginationParams @params)
            => Ok(await _blogPostService.GetAllAsync(@params, blog => blog.UserId == id));
    }
}