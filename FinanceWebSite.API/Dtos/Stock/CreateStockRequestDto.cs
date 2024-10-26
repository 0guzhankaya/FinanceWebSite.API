﻿using System.ComponentModel.DataAnnotations;

namespace FinanceWebSite.API.Dtos.Stock
{
	public class CreateStockRequestDto
	{
		[Required]
		[MaxLength(10, ErrorMessage = "Symbol cannot be over 10 over characters")]
		public string Symbol { get; set; } = string.Empty;

		[Required]
		[MaxLength(10, ErrorMessage = "Company Name cannot be over 10 over characters")]
		public string CompanyName { get; set; } = string.Empty;

		[Required]
		[Range(1, 1000000000)]
		public decimal Purchase { get; set; }

		[Required]
		[Range(0.001, 100)]
		public decimal LastDiv { get; set; }

		[Required]
		[MaxLength(20, ErrorMessage = "Industry can not be over 20 characters")]
		public string Industry { get; set; } = string.Empty;

		[Range(1, 5000000000)]
		public long MarketCap { get; set; }
	}
}
