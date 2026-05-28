namespace WebApplication4.Entities;

public class Rentals
{
    public int RentalId { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime RentedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }

    public virtual Books Books { get; set; } = null!;
    public virtual Users User { get; set; } = null!;
}