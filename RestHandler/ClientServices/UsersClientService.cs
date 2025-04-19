using RestHandler.Helper;
using RestHandler.Models;

namespace RestHandler.ClientServices
{
	public class UsersClientService : IUsersClientService
	{
		private readonly HttpClient httpClient;

		public UsersClientService(HttpClient httpClientFactory)
		{
			this.httpClient = httpClientFactory;
		}

		public async Task<HttpResponseMessage> GetUser(int id)
		{
			var uri = $"api/users/{id}";
			return await httpClient.GetAsync(uri);
		}

		public async Task<HttpResponseMessage> PostUser(UserRequest userRequest)
		{
			var uri = "api/users";
			return await httpClient.PostAsJsonAsync<UserRequest>(uri, userRequest);
		}

		public async Task<HttpResponseMessage> PutUser(int id, UserRequest userRequest)
		{
			var uri = $"api/users/{id}";
			return await httpClient.PutAsJsonAsync<UserRequest>(uri, userRequest);
		}
	}
}
