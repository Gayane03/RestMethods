using RestHandler.Models;

namespace RestHandler.ClientServices
{
	public interface IUsersClientService
	{
		Task<HttpResponseMessage> GetUser(int id);
		Task<HttpResponseMessage> PostUser(UserRequest userRequest);
		Task<HttpResponseMessage> PutUser(int id, UserRequest userRequest);
	}
}
