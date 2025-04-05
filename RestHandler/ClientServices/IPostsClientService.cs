using RestHandler.Models;

namespace RestHandler.ClientServices
{
	public interface IPostsClientService
	{
	    Task<HttpResponseMessage> GetPostsWithFilter(PostFilter postFilter);
		Task<HttpResponseMessage> GetPost(int id);
		Task<HttpResponseMessage> DeletePost(int id);
	}
}
