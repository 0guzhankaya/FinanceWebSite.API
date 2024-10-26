using FinanceWebSite.API.Models;

namespace FinanceWebSite.API.Contracts
{
	public interface ITokenService
	{
		string CreateToken(AppUser user);
	}
}
