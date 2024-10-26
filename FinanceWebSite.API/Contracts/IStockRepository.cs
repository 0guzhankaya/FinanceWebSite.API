using FinanceWebSite.API.Dtos.Stock;
using FinanceWebSite.API.Models;

namespace FinanceWebSite.API.Contracts
{
	public interface IStockRepository
	{
		Task<List<Stock>> GetAllAsync();
		Task<Stock?> GetByIdAsync(int id); // FirstOrDefault
		Task<Stock> CreateAsync(Stock stockModel);
		Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
		Task<Stock?> DeleteAsync(int id);
		Task<bool> StockExists(int id);
	}
}
