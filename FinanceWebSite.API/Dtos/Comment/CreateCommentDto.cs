using System.ComponentModel.DataAnnotations;

namespace FinanceWebSite.API.Dtos.Comment
{
	public class CreateCommentDto
	{
		[Required]
		[MinLength(5, ErrorMessage = "Title must be min. 5 characters")]
		[MaxLength(280, ErrorMessage = "Title connot be over 280 characters")]
		public string Title { get; set; } = string.Empty;

		[Required]
		[MinLength(5, ErrorMessage = "Content must be min. 5 characters")]
		[MaxLength(280, ErrorMessage = "Content connot be over 280 characters")]
		public string Content { get; set; } = string.Empty;
	}
}
