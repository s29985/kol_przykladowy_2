using WebApplication4.Entities;

namespace WebApplication4.DTO;

public record BookResponse(
    int Id,
    string Title,
    string AuthorFirstName,
    string AuthorLastName,
    List<string> Genres,
    List<RentalResponse> Rentals
    );