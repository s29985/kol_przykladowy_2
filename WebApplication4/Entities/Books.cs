namespace WebApplication4.Entities;

public class Books
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorFirstName { get; set; } = string.Empty;
    public string AuthorLastName { get; set; } = string.Empty;
    
    
    public virtual ICollection<Genres> Genres { get; set; } =  new List<Genres>();
    public virtual ICollection<Rentals> Rentals { get; set; } =  new List<Rentals>();
}