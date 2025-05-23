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
            if (user == null)
                return new List<Stock>();

            return await _context
                .Portfolios.Include(p => p.Stock)
                .Where(p => p.AppUserId == user.Id)
                .Select(p => p.Stock)
                .ToListAsync();
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(p =>
                p.AppUserId == appUser.Id && p.Stock.Symbol.ToLower() == symbol.ToLower()
            );

            if (portfolioModel == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }
    }
}
