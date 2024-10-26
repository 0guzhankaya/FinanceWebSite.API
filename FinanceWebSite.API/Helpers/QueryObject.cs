namespace FinanceWebSite.API.Helpers
{
	public class QueryObject
	{
		// Filtering
		public string? Symbol { get; set; } = null;
		public string? CompanyName { get; set; } = null;

		// Sorting
		public string? SortBy { get; set; } = null;
		public bool IsDecsending { get; set; } = false;
	}
}
