using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceWebSite.API.Models
{
	[Table("Portfolios")]
	public class Portfolio
	{
		public string AppUserId { get; set; }
		public int StockId { get; set; }
		public AppUser AppUser { get; set; } // Navigation Property
		public Stock Stock { get; set; } // Navigation Property
	}
}
