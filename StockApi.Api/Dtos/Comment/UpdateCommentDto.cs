using System.ComponentModel.DataAnnotations;

namespace StockApi.Api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "Title must be less than 280 characters long.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "Content must be less than 280 characters long.")]
        public string Content { get; set; } = string.Empty;
    }
}