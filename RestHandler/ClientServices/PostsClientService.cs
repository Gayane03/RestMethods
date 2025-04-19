using Flurl;
using RestHandler.Helper;
using RestHandler.Models;

namespace RestHandler.ClientServices
{
	public class PostsClientService : IPostsClientService
	{
		private readonly HttpClient httpClient;
		private readonly IResponseMessageUtile responseMessageUtile;
		public PostsClientService(IHttpClientFactory httpClientFactory, IResponseMessageUtile responseMessageUtile)
		{
			this.httpClient = httpClientFactory.CreateClient(BackendApi.Posts);
			this.responseMessageUtile = responseMessageUtile;
		}

		public async Task<(T, string)> GetPostsWithFilter<T>(PostFilter postFilter)
		{
			var uri = "posts".SetQueryParams(postFilter);
			var response = await httpClient.GetAsync(uri);

			return await responseMessageUtile.HandleResponse<T>(response);
		}

		public async Task<(T, string)> GetPost<T>(int id)
		{
			var uri = $"posts/{id}";
			var response = await httpClient.GetAsync(uri);

			return await responseMessageUtile.HandleResponse<T>(response);
		}

		public async Task DeletePost(int id)
		{
			var uri = $"posts/{id}";
			await httpClient.DeleteAsync(uri);
		}
	}
}
