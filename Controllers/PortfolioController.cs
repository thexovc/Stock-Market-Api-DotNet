using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockMarket.Extensions;
using stockMarket.Interfaces;
using stockModel.Models;

namespace stockMarket.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepo,
            IPortfolioRepository portfolioRepo
        )
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();

            Console.WriteLine(username);
            if (string.IsNullOrEmpty(username))
                return Unauthorized("User not found");

            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
                return NotFound("User not found");

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost("{symbol}")]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return BadRequest("Symbol is required");

            var username = User.GetUsername();

            Console.WriteLine(username);

            if (string.IsNullOrEmpty(username))
                return Unauthorized("User not found");

            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
                return NotFound("User not found");

            var stock = await _stockRepo.GetStockBySymbol(symbol);
            if (stock == null)
                return NotFound($"Stock with symbol '{symbol}' not found");

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if (userPortfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Stock already exists in portfolio");

            var portfolioModel = new Portfolio { AppUserId = appUser.Id, StockId = stock.Id };

            var result = await _portfolioRepo.CreateAsync(portfolioModel);
            if (result == null)
                return StatusCode(500, "Failed to add stock to portfolio");

            return CreatedAtAction(nameof(GetUserPortfolio), new { id = result.StockId }, result);
        }
    }
}
