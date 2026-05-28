using System.ComponentModel.DataAnnotations;
namespace WebApplication4.DTO;


public record BooksRequest
(
    [Required, MaxLength(200)] string Title,
    [Required, MaxLength(50)] string AuthorFirstName,
    [Required, MaxLength(200)] string AuthorLastName,
    [Required] ICollection<string> Genres
);