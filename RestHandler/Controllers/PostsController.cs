using Microsoft.AspNetCore.Mvc;
using RestHandler.ClientServices;
using RestHandler.Helper;
using RestHandler.Models;

namespace RestHandler.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PostsController : ControllerBase
	{

		private readonly IPostsClientService postsClientService;

		public PostsController(IPostsClientService postsClientService, IResponseMessageUtile responseMessageUtile)
		{
			this.postsClientService = postsClientService;	
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts([FromQuery] PostFilter postFilter)
		{
			try
			{
				var (response, error) = await postsClientService.GetPostsWithFilter<IEnumerable<PostResponse>>(postFilter);

				if (error is not null)
				{
					return NotFound(error);
				}

				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}


		[HttpGet("{id:int}")]
		public async Task<ActionResult<PostResponse>> GetPost(int id)
		{
			try
			{
				var (response, error) = await postsClientService.GetPost<PostResponse>(id);

				if (error is not null)
				{
					return NotFound(error);
				}

				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}


		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeletePost(int id)
		{
			try
			{
				await postsClientService.DeletePost(id);		
				return NoContent();			
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}
	}
}
