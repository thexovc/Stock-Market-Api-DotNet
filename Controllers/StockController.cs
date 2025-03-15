using Microsoft.AspNetCore.Mvc;
using stockMarket.Dtos.Stock;
using stockMarket.Mappers;
using stockModel.Data;

namespace stockMarket.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDTO();

            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

    }
}