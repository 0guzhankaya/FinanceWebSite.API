using FinanceWebSite.API.Models;

namespace FinanceWebSite.API.Contracts
{
	public interface IPortfolioRepository
	{
		Task<List<Stock>> GetUserPortfolio(AppUser user);
		Task<Portfolio> CreateAsync (Portfolio portfolio);
		Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);
	}
}
