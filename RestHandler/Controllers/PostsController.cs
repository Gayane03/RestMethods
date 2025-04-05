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
		private readonly IResponseMessageUtile responseMessageUtile;

		public PostsController(IPostsClientService postsClientService, IResponseMessageUtile responseMessageUtile)
		{
			this.postsClientService = postsClientService;	
			this.responseMessageUtile = responseMessageUtile;	
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts([FromQuery] PostFilter postFilter)
		{
			try
			{
				var response = await postsClientService.GetPostsWithFilter(postFilter);
				var (result, error) = await responseMessageUtile.HandleResponse<IEnumerable<PostResponse>>(response);

				if (error is not null)
				{
					return StatusCode((int)response.StatusCode,error);
				}

				return Ok(result);
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
				var response = await postsClientService.GetPost(id);
				var (result, error) = await responseMessageUtile.HandleResponse<PostResponse>(response);

				if (error is not null)
				{
					return StatusCode((int)response.StatusCode, error);
				}

				return Ok(result);
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
				var response = await postsClientService.DeletePost(id);		
				return StatusCode((int)response.StatusCode);			
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}
	}
}
