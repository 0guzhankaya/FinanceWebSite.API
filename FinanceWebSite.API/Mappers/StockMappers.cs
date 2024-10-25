using FinanceWebSite.API.Dtos.Stock;
using FinanceWebSite.API.Models;

namespace FinanceWebSite.API.Mappers
{
	public static class StockMappers
	{
		public static StockDto ToStockDto(this Stock stockModel)
		{
			return new StockDto
			{
				Id = stockModel.Id,
				Symbol = stockModel.Symbol,
				CompanyName = stockModel.CompanyName,
				Purchase = stockModel.Purchase,
				LastDiv = stockModel.LastDiv,
				Industry = stockModel.Industry,
				MarketCap = stockModel.MarketCap,
			};
		}
	}
}
