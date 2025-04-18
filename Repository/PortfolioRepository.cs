using Microsoft.EntityFrameworkCore;
using stockMarket.Dtos.Stock;
using stockMarket.Helpers;
using stockModel.Data;
using stockModel.Models;

namespace stockMarket.Interfaces
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios
                .Where(p => p.AppUserId == user.Id)
                .Select(p => new Stock
                {
                    Id = p.StockId,
                    Symbol = p.Stock.Symbol,
                    Purchase = p.Stock.Purchase,
                    MarketCap = p.Stock.MarketCap,
                    LastDiv = p.Stock.LastDiv,
                    Industry = p.Stock.Industry,
                    CompanyName = p.Stock.CompanyName
                })
                .ToListAsync();
        }
    }
}
