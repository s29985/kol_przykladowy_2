namespace WebApplication4.DTO;

public record RentalResponse(
    string UserName,
    string? UserPhone,
    string? UserEmail,
    DateTime RentDate,
    DateTime? ReturnDate);