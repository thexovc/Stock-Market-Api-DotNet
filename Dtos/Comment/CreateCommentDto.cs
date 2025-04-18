using System.ComponentModel.DataAnnotations;

namespace stockMarket.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Contenet must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Contenet cannot be over 280 characters")]

        public string Content { get; set; } = string.Empty;
    }
}