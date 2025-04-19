using Flurl;
using RestHandler.Helper;
using RestHandler.Models;

namespace RestHandler.ClientServices
{
	public class PostsClientService : IPostsClientService
	{
		private readonly HttpClient httpClient;
		public PostsClientService(HttpClient httpClientFactory)
		{
			this.httpClient = httpClientFactory;
		}

		public async Task<(T, string)> GetPostsWithFilter<T>(PostFilter postFilter)
		{
			var uri = "posts".SetQueryParams(postFilter);
			var response = await httpClient.GetAsync(uri);

			return await ResponseMessageUtile.HandleResponse<T>(response);
		}

		public async Task<(T, string)> GetPost<T>(int id)
		{
			var uri = $"posts/{id}";
			var response = await httpClient.GetAsync(uri);

			return await ResponseMessageUtile.HandleResponse<T>(response);
		}

		public async Task DeletePost(int id)
		{
			var uri = $"posts/{id}";
			await httpClient.DeleteAsync(uri);
		}
	}
}
