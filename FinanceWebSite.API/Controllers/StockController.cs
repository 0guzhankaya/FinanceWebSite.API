﻿using FinanceWebSite.API.Contracts;
using FinanceWebSite.API.Data;
using FinanceWebSite.API.Dtos.Stock;
using FinanceWebSite.API.Helpers;
using FinanceWebSite.API.Mappers;
using Microsoft.AspNetCore.Authorization;
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
		private readonly IStockRepository _stockRepository;

		public StockController(ApplicationDbContext context, IStockRepository stockRepository)
		{
			_stockRepository = stockRepository;
			_context = context;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var stocks = await _stockRepository.GetAllAsync(query);
			var stockDto = stocks.Select(s => s.ToStockDto()).ToList();

			return Ok(stockDto);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var stock = await _stockRepository.GetByIdAsync(id);

			if (stock == null)
				return NotFound();

			return Ok(stock.ToStockDto());
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var stockModel = stockDto.ToStockFromCreateDto();
			await _stockRepository.CreateAsync(stockModel);

			return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
		}

		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var stockModel = await _stockRepository.UpdateAsync(id, updateDto);

			if (stockModel == null)
				return NotFound();

			return Ok(stockModel.ToStockDto());
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var stockModel = await _stockRepository.DeleteAsync(id);
			if (stockModel == null)
				return NotFound();

			return NoContent();
		}
	}
}
