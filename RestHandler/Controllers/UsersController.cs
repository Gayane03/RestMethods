using Microsoft.AspNetCore.Mvc;
using RestHandler.ClientServices;
using RestHandler.Helper;
using RestHandler.Models;

namespace RestHandler.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersClientService usersClientService;
		private readonly IResponseMessageUtile responseMessageUtile;

		public UsersController(IUsersClientService usersClientService, IResponseMessageUtile responseMessageUtile)
		{
			this.usersClientService = usersClientService;
			this.responseMessageUtile = responseMessageUtile;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<UserResponse>> GetUser(int id)
		{
			try
			{
				var response = await usersClientService.GetUser(id);
				var (result, error) = await responseMessageUtile.HandleResponse<UserResponse>(response);

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

		[HttpPost]
		public async Task<ActionResult<UserPaginationResponse>> PostUsers([FromBody] UserRequest userRequest)
		{
			try
			{
				var response = await usersClientService.PostUser(userRequest);
				var (result, error) = await responseMessageUtile.HandleResponse<UserPaginationResponse>(response);

				if (error is not null)
				{
					return StatusCode((int)response.StatusCode, error);
				}

				return CreatedAtAction(nameof(GetUser),result);
			}
			catch (Exception)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}


		[HttpPut("{id:int}")]
		public async Task<ActionResult<UserResponse>> PutUser(int id, [FromBody] UserRequest userRequest)
		{
			try
			{
				var response = await usersClientService.PutUser(id, userRequest);
				return StatusCode((int)response.StatusCode);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}

	}
}
