namespace WebApplication4.Entities;

public class Genres
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<Books> Books { get; set; } =  new List<Books>();
}