using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceWebSite.API.Models
{
	[Table("Comments")]
	public class Comment
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime CreateOn { get; set; } = DateTime.Now;
		public int? StockId { get; set; } // Foreign Key Property
		public Stock? Stock { get; set; } // Navigation Property
	}
}
