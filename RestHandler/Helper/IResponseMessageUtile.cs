namespace RestHandler.Helper
{
	public interface IResponseMessageUtile
	{
		Task<(T? response, string? error)> HandleResponse<T>(HttpResponseMessage? response);
	}
}
