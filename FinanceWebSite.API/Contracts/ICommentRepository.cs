using FinanceWebSite.API.Dtos.Comment;
using FinanceWebSite.API.Models;

namespace FinanceWebSite.API.Contracts
{
	public interface ICommentRepository
	{
		Task<List<Comment>> GetAllAsync();
		Task<Comment?> GetByIdAsync(int id);
		Task<Comment> CreateAsync(Comment commentModel);
		Task<Comment?> UpdateAsync(int id, Comment commentModel);
		Task<Comment?> DeleteAsync(int id);	
	}
}
