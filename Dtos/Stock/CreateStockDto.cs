using System.ComponentModel.DataAnnotations;

namespace stockMarket.Dtos.Stock
{
    public class CreateStockDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Symbol must be at least 3 characters")]
        [MaxLength(280, ErrorMessage = "Symbol cannot be over 280 characters")]

        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Company Name must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Company Name cannot be over 280 characters")]

        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        [MinLength(5, ErrorMessage = "Industry must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Industry cannot be over 280 characters")]
        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
    }
}