namespace RestHandler.Helper
{
	public class ResponseMessageUtile : IResponseMessageUtile
	{

		private readonly ILogger<ResponseMessageUtile> logger;

		public ResponseMessageUtile(ILogger<ResponseMessageUtile> logger)
		{
			this.logger = logger;
		}

		public async Task<(T? response, string? error)> HandleResponse<T>(HttpResponseMessage? response)
		{
			string? errorMessage;

			if (response == null)
			{
				errorMessage = "Response is null.";
				logger.LogError(errorMessage);
				return (response: default(T), error: errorMessage);
			}

			if (!response.IsSuccessStatusCode)
			{
				errorMessage = await response.Content.ReadAsStringAsync();
				logger.LogError(errorMessage);
				return (response: default(T), error: errorMessage);
			}

			try
			{
				var result = await response!.Content.ReadFromJsonAsync<T>();
				if (result == null)
				{
					errorMessage = "Json deserialization result is null.";
					logger.LogError(errorMessage);
					return (response: default(T), error: errorMessage);
				}

				return (response: result, error: null);
			}
			catch (Exception ex)
			{
				errorMessage = $"Error during JSON deserialization: {ex.Message}";
				logger.LogError(errorMessage);
				return (Response: default(T), Error: errorMessage);
			}

		}
	}
}
