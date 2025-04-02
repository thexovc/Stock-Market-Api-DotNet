using stockMarket.Dtos.Stock;
using stockMarket.Helpers;
using stockModel.Models;

namespace stockMarket.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stock);

        Task<Stock> UpdateAsync(int id, UpdateStockDto stock);

        Task<Stock> DeleteAsync(int id);

        Task<bool> StockExists(int id);
    }
}
