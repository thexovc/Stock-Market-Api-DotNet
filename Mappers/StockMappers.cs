using stockMarket.Dtos.Stock;
using stockModel.Models;

namespace stockMarket.Mappers
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
                MarketCap = stockModel.MarketCap
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockDto createStockDto)
        {
            return new Stock
            {
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Purchase = createStockDto.Purchase,
                LastDiv = createStockDto.LastDiv,
                Industry = createStockDto.Industry,
                MarketCap = createStockDto.MarketCap
            };
        }
    }
}