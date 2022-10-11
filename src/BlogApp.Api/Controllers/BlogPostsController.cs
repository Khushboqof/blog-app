using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers
{
    [Route("api/blogposts")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateAsync([FromForm] BlogCreateViewModel viewModel)
            => Ok(await _blogPostService.CreateAsync(viewModel));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
            => Ok(await _blogPostService.GetAsync(o => o.Id == id));

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _blogPostService.DeleteAsync(o => o.Id == id));

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] BlogCreateViewModel viewModel)
            => Ok(await _blogPostService.UpdateAsync(id, viewModel));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _blogPostService.GetAllAsync(@params));
    }
}
