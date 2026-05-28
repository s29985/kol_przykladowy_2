using Microsoft.EntityFrameworkCore;
using WebApplication4.Entities;

namespace WebApplication4.Infrastructure;

public class DatabaseContext : DbContext
{
    //required
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    
    // тут мы пишем название наших таблиц 
    public virtual DbSet<Books> Books { get; set; }
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Genres> Genres { get; set; }
    public virtual DbSet<Rentals> Rentals { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Books>(b =>
        {
            b.HasKey(k => k.Id);
            b.Property(k => k.Title).HasMaxLength(200).IsRequired();
            b.Property(k => k.AuthorFirstName).HasMaxLength(50).IsRequired();
            b.Property(k => k.AuthorLastName).HasMaxLength(200).IsRequired();


            //созаем папку много на много
            b.HasMany(k => k.Genres)
                .WithMany(k => k.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenres",
                    j => j.HasOne<Genres>().WithMany().HasForeignKey("GenresId"),
                    j => j.HasOne<Books>().WithMany().HasForeignKey("BooksId")
                );
        });



        modelBuilder.Entity<Users>(u =>
        {
            u.HasKey(k => k.Id); 
            u.Property(k => k.FirstName).HasMaxLength(50).IsRequired();
            u.Property(k => k.LastName).HasMaxLength(50).IsRequired();
            u.Property(k => k.Phone).HasColumnType("char(9)");
            u.Property(k => k.Email).HasMaxLength(200);
        });

        
        
        modelBuilder.Entity<Genres>(g =>
        {
            g.HasKey(k => k.Id);
            g.Property(k => k.Name).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Rentals>(r =>
        {
            r.HasKey(k => k.RentalId);
            r.Property(k => k.RentedAt).IsRequired();
            r.Property(k => k.ReturnedAt);
            
            r.HasOne(k => k.User)
                .WithMany(k => k.Rentals)
                .HasForeignKey(k => k.UserId);
            
            r.HasOne(k => k.Books)
                .WithMany(k => k.Rentals)
                .HasForeignKey(k => k.BookId);
        });

    }
}