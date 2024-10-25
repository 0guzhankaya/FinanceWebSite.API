using FinanceWebSite.API.Data;
using FinanceWebSite.API.Dtos.Stock;
using FinanceWebSite.API.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<IActionResult> GetAll()
		{
			var stocks = await _context.Stocks.ToListAsync();
			var stockDto = stocks.Select(s => s.ToStockDto());

			return Ok(stockDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			// if you have only id parameter Find() method is more eff than SingleOrDefault() Method
			// because Find() method searched directly on Primary Key.
			var stock = await _context.Stocks.FindAsync(id);

			if (stock == null)
				return NotFound();

			return Ok(stock.ToStockDto());
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
		{
			var stockModel = stockDto.ToStockFromCreateDto();
			await _context.Stocks.AddAsync(stockModel);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
		{
			var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

			if (stockModel == null)
				return NotFound();

			stockModel.Symbol = updateDto.Symbol;
			stockModel.CompanyName = updateDto.CompanyName;
			stockModel.Purchase = updateDto.Purchase;
			stockModel.LastDiv = updateDto.LastDiv;
			stockModel.Industry = updateDto.Industry;
			stockModel.MarketCap = updateDto.MarketCap;

			await _context.SaveChangesAsync();
			return Ok(stockModel.ToStockDto());
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
			if (stockModel == null)
				return NotFound();

			_context.Stocks.Remove(stockModel); // Remove() is not async.
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
