using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesBackend.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "Director is required.")]
        public string? Director { get; set; }
        [Required(ErrorMessage = "At least one genre is required.")]
        public string? Genre { get; set; }
        [Required(ErrorMessage = "Release year is required.")]
        [Range(1895, 2100, ErrorMessage = "Release year must be between 1895 and 2100.")]
        public int? ReleaseYear { get; set; }
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? Img { get; set; }
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string? Description { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
