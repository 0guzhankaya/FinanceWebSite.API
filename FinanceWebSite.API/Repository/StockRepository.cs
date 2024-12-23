﻿using FinanceWebSite.API.Contracts;
using FinanceWebSite.API.Data;
using FinanceWebSite.API.Dtos.Stock;
using FinanceWebSite.API.Helpers;
using FinanceWebSite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceWebSite.API.Repository
{
	public class StockRepository : IStockRepository
	{
		private readonly ApplicationDbContext _context;

		public StockRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Stock> CreateAsync(Stock stockModel)
		{
			await _context.Stocks.AddAsync(stockModel);
			await _context.SaveChangesAsync();
			return stockModel;
		}

		public async Task<Stock?> DeleteAsync(int id)
		{
			// FirstOrDefault can return null.
			var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
			if (stockModel == null)
				return null;

			_context.Stocks.Remove(stockModel); // Remove() is not async.
			await _context.SaveChangesAsync();
			return stockModel;
		}

		public async Task<List<Stock>> GetAllAsync(QueryObject query)
		{
			var stocks = _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

			if (!string.IsNullOrWhiteSpace(query.CompanyName))
			{
				stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
			}

			if(!string.IsNullOrWhiteSpace(query.Symbol))
			{
				stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
			}

			if (!string.IsNullOrWhiteSpace(query.SortBy))
			{
				if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
				{
					stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
				}
			}

			var skipNumber = (query.PageNumber -1) * query.PageSize;

			return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
		}

		public async Task<Stock?> GetByIdAsync(int id)
		{
			// if you have only id parameter Find() method is more eff than SingleOrDefault() Method
			// because Find() method searched directly on Primary Key.
			return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
		}

		public async Task<Stock?> GetBySymbolAsync(string symbol)
		{
			return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
		}

		public Task<bool> StockExists(int id)
		{
			return _context.Stocks.AnyAsync(s => s.Id == id);
		}

		public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
		{
			var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

			if (existingStock == null)
				return null;

			existingStock.Symbol = stockDto.Symbol;
			existingStock.CompanyName = stockDto.CompanyName;
			existingStock.Purchase = stockDto.Purchase;
			existingStock.LastDiv = stockDto.LastDiv;
			existingStock.Industry = stockDto.Industry;
			existingStock.MarketCap = stockDto.MarketCap;

			await _context.SaveChangesAsync();
			return existingStock;
		}
	}
}
