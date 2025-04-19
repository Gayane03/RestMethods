namespace RestHandler.Helper
{
	public static class ResponseMessageUtile 
	{
		public static async Task<(T? response, string? error)> HandleResponse<T>(HttpResponseMessage? response)
		{
			string? errorMessage;

			if (response == null)
			{
				errorMessage = "Response is null.";
				return (response: default(T), error: errorMessage);
			}

			if (!response.IsSuccessStatusCode)
			{
				errorMessage = await response.Content.ReadAsStringAsync();
				return (response: default(T), error: errorMessage);
			}

			try
			{
				var result = await response!.Content.ReadFromJsonAsync<T>();
				if (result == null)
				{
					errorMessage = "Json deserialization result is null.";
					return (response: default(T), error: errorMessage);
				}

				return (response: result, error: null);
			}
			catch (Exception ex)
			{
				errorMessage = $"Error during JSON deserialization: {ex.Message}";
				return (Response: default(T), Error: errorMessage);
			}

		}
	}
}
