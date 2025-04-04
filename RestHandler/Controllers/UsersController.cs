using Microsoft.AspNetCore.Mvc;
using RestHandler.Models;

namespace RestHandler.Controllers
{
	[ApiController]
	public class UsersController : ControllerBase
	{

		[HttpPost]
		public ActionResult<UserPaginationResponse> PostUsers([FromBody] IEnumerable<UserRequest> usersRequest)
		{
			try
			{
				var userPaginationResponse = new UserPaginationResponse
				{  
					Page = 1,
					PerPage = 6,
					Total = 12,
					TotalPages = 2,
					Data = new List<User>(),
					Support = new Support
					{
						Url = "https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral",
						Text = "Tired of writing endless social media content? Let Content Caddy generate it for you."
					}
				};


				foreach (var user in usersRequest)
				{
					userPaginationResponse.Data.Add(new User() { Id = ++i, Email = user.Email , FirstName = user.FirstName, LastName = user.LastName,Avatar = user.Avatar });
				}
			catch (Exception)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}


		[HttpPut("{id:int}")]
		public ActionResult<UserResponse> PutUser(int id, [FromBody] UserRequest userRequest)
		{
			try
			{
				var user = users.FirstOrDefault(user => user.Id == id);

				if(user is null)
				{
					return NoContent();
				}

				user.Email = userRequest.Email;
				user.FirstName = userRequest.FirstName;
				user.LastName = userRequest.LastName;
				user.Avatar = userRequest.Avatar;

				var userResponse = new UserResponse
				{
					Data = user,
					Support = new Support
					{
						Url = "https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral",
						Text = "Tired of writing endless social media content? Let Content Caddy generate it for you."
					}
				};

				return Ok(userResponse);
			}
			catch (Exception)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}

	}
}
