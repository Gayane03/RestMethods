using Microsoft.AspNetCore.Mvc;
using RestHandler.Models;

namespace RestHandler.Controllers
{
	[Route("api/posts")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IEnumerable<PostResponse> posts;

		public PostsController()
		{
			posts = new List<PostResponse>() {
			   new PostResponse{UserId = 1, Id = 1, Title ="qui est esse", Body="est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla" },
			   new PostResponse{UserId = 2, Id = 2, Title ="qui est esse", Body="est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea possimus qui neque nisi nulla" },
			   new PostResponse{UserId = 1, Id = 3, Title ="ea molestias quasi exercitationem repellat qui ipsa sit aut", Body="et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut" },
				};
		}

		[HttpGet]
		public ActionResult<IEnumerable<PostResponse>> GetPosts([FromQuery] PostFilter postFilter)
		{
			try
			{
				var userId = postFilter.userId;
				var decryptedTitle = postFilter.title.Replace("%20", " ");

				var filteredPosts = posts.Where(post => post.UserId == userId && post.Title.Contains(decryptedTitle));

				if (filteredPosts is null || !filteredPosts.Any())
				{
					return NoContent();
				}

				return Ok(filteredPosts);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}

		}


		[HttpGet("{id:int}")]
		public ActionResult<PostResponse> GetPost(int id)
		{
			try
			{

				var filteredPosts = posts.Where(post => post.Id == id);

				if (filteredPosts is null || !filteredPosts.Any())
				{
					return NoContent();
				}

				return Ok(filteredPosts);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}

		}


		[HttpDelete("{id:int}")]
		public ActionResult<PostResponse> DeletePost(int id)
		{
			try
			{
				var deletedPost = posts.FirstOrDefault(post => post.Id == id);

				if (deletedPost is null)
				{
					return NoContent();
				}

				//var currentPosts = posts.Where(post => post != deletingPost);

				return Ok(deletedPost);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}

		}
	}
}
