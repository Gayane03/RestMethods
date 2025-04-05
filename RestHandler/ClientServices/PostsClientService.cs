using Flurl;
using RestHandler.Helper;
using RestHandler.Models;

namespace RestHandler.ClientServices
{
	public class PostsClientService : IPostsClientService
	{
		private readonly HttpClient httpClient;

		public PostsClientService(IHttpClientFactory httpClientFactory)
		{
			this.httpClient = httpClientFactory.CreateClient(BackendApi.Posts);
		}

		public async Task<HttpResponseMessage> GetPostsWithFilter(PostFilter postFilter)
		{
			var uri = "posts".SetQueryParams(postFilter);
			return await httpClient.GetAsync(uri);
		}

		public async Task<HttpResponseMessage> GetPost(int id)
		{
			var uri = $"posts/{id}";
			return await httpClient.GetAsync(uri);
		}

		public async Task<HttpResponseMessage> DeletePost(int id)
		{
			var uri = $"posts/{id}";
			return await httpClient.DeleteAsync(uri);
		}

	}
}
