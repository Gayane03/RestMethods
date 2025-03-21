namespace RestHandler.Models
{
	public class UserPaginationResponse
	{
		public int Page { get; set; }	
		public int PerPage { get; set; }
		public int Total { get; set; }
		public int TotalPages { get; set; }
		public List<User> Data { get; set; }
		public Support Support { get; set; }
	}
}
