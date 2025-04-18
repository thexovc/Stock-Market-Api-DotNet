using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stockModel.Models
{
    [Table("Portfolios")]
    
    public class Portfolio
    {
        [Required]
        public string AppUserId { get; set; } = string.Empty;

        [Required]
        public int StockId { get; set; }

        [Required]
        public AppUser AppUser { get; set; } = null!;

        [Required]
        public Stock Stock { get; set; } = null!;
        
        
    }
}