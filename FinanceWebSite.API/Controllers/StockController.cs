using FinanceWebSite.API.Data;
using FinanceWebSite.API.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebSite.API.Controllers
{
	[Route("api/stock")]
	[ApiController]
	public class StockController : ControllerBase
	{
        private readonly ApplicationDbContext _context;

		public StockController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var stocks = _context.Stocks.ToList()
			.Select(s => s.ToStockDto());

			return Ok(stocks);
		}

		[HttpGet("{id}")]
		public IActionResult GetById([FromRoute] int id)
		{
			// if you have only id parameter Find() method is more eff than SingleOrDefault() Method
			// because Find() method searched directly on Primary Key.
			var stock = _context.Stocks.Find(id);

			if (stock == null)
				return NotFound();

			return Ok(stock.ToStockDto());
		}
	}
}
