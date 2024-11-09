using FinanceWebSite.API.Contracts;
using FinanceWebSite.API.Extensions;
using FinanceWebSite.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebSite.API.Controllers
{
	[Route("api/portfolio")]
	[ApiController]
	public class PortfolioController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IStockRepository _stockRepository;
		private readonly IPortfolioRepository _portfolioRepository;

		public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
		{
			_userManager = userManager;
			_stockRepository = stockRepository;
			_portfolioRepository = portfolioRepository;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetUserPortfolio()
		{
			var userName = User.GetUsername();
			var appUser = await _userManager.FindByNameAsync(userName);
			var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
			return Ok(userPortfolio);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> AddPortfolio(string symbol)
		{
			var userName = User.GetUsername();
			var appUser = await _userManager.FindByNameAsync(userName);

			var stock = await _stockRepository.GetBySymbolAsync(symbol);

			if (stock == null) return BadRequest("Stock not found!");

			var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

			if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio!");

			var portfolioModel = new Portfolio
			{
				StockId = stock.Id,
				AppUserId = appUser.Id,
			};

			await _portfolioRepository.CreateAsync(portfolioModel);

			if (portfolioModel is null)
				return StatusCode(StatusCodes.Status500InternalServerError, "Could not create!");
			else
				return Created();
		}

		[Authorize]
		[HttpDelete]
		public async Task<IActionResult> DeletePortfolio(string symbol)
		{
			var userName = User.GetUsername();
			var appUser = await _userManager.FindByNameAsync(userName);

			var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
			var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

			if (filteredStock.Count() == 1)
				await _portfolioRepository.DeletePortfolio(appUser, symbol);
			else
				return BadRequest("Stock not in your portfolio!");
			
			return Ok("Deleted successfully!");
		}
	}
}
