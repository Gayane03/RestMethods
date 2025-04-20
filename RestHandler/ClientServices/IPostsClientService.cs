using RestHandler.Models;

namespace RestHandler.ClientServices
{
	public interface IPostsClientService
	{
	    Task<(T,string)> GetPostsWithFilter<T>(PostFilter postFilter);
		Task<(T, string)> GetPost<T>(int id);
		Task DeletePost(int id);
	}
}
