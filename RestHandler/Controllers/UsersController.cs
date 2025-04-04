using Microsoft.AspNetCore.Mvc;
using RestHandler.Models;

namespace RestHandler.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private List<User> users;

		public UsersController()
		{
			users = new List<User>() {
					new User { Id = 1, Email = "george.bluth@reqres.in", FirstName = "George", LastName = "Bluth", Avatar = "https://reqres.in/img/faces/1-image.jpg" },
					new User { Id = 2, Email = "janet.weaver@reqres.in", FirstName = "Janet", LastName = "Weaver", Avatar = "https://reqres.in/img/faces/2-image.jpg" },
					new User { Id = 3, Email = "emma.wong@reqres.in", FirstName = "Emma", LastName = "Wong", Avatar = "https://reqres.in/img/faces/3-image.jpg" },
					new User { Id = 4, Email = "eve.holt@reqres.in", FirstName = "Eve", LastName = "Holt", Avatar = "https://reqres.in/img/faces/4-image.jpg" },
					new User { Id = 5, Email = "charles.morris@reqres.in", FirstName = "Charles", LastName = "Morris", Avatar = "https://reqres.in/img/faces/5-image.jpg" },
					new User { Id = 6, Email = "tracey.ramos@reqres.in", FirstName = "Tracey", LastName = "Ramos", Avatar = "https://reqres.in/img/faces/6-image.jpg" }
			};
		}

		[HttpGet("{id:int}")]
		public ActionResult<User> GetUser(int id)
		{
			try
			{
				var user = users.FirstOrDefault(post => post.Id == id);
				if (user is null)
				{
					return NotFound();
				}

				return user;
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}



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
