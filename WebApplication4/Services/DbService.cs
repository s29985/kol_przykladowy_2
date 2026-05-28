using Microsoft.EntityFrameworkCore;
using WebApplication4.DTO;
using WebApplication4.Entities; // Твоя папка с моделями
using WebApplication4.Infrastructure; // Твоя папка с DatabaseContext

namespace WebApplication4.Services;

public class DbService : IDbService
{
    //default
    private readonly DatabaseContext _context;

    // Внедряем базу данных
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    // --- РЕАЛИЗАЦИЯ GET ---
    
    //идея простая мы берем список книг и кто их брал
    public async Task<List<BookResponse>> GetBooksAsync(string? title, CancellationToken cancellationToken)
    {
        return await _context.Books
            .Where(b => title == null || b.Title.Contains(title)) // Фильтр по названию
            .Select(b => new BookResponse(
                b.Id,
                b.Title,
                b.AuthorFirstName,
                b.AuthorLastName,
                b.Genres.Select(g => g.Name).ToList(), // Достаем только названия жанров
                b.Rentals.Select(r => new RentalResponse(
                    $"{r.User.FirstName} {r.User.LastName}", // Склеиваем имя и фамилию
                    r.User.Phone,
                    r.User.Email,
                    r.RentedAt,
                    r.ReturnedAt
                )).ToList()
            ))
            .ToListAsync(cancellationToken);
    }
    
    // --- РЕАЛИЗАЦИЯ POST ---
    public async Task<int> AddBookAsync(BooksRequest request, CancellationToken cancellationToken)
    {
        // 1. Создаем сырую книгу
        var newBook = new Books
        {
            Title = request.Title,
            AuthorFirstName = request.AuthorFirstName,
            AuthorLastName = request.AuthorLastName
        };

        // 2. Разбираемся с жанрами (магия Многие-ко-Многим)
        foreach (var genreName in request.Genres)
        {
            var existingGenre = await _context.Genres
                .FirstOrDefaultAsync(g => g.Name == genreName, cancellationToken);

            if (existingGenre != null)
            {
                newBook.Genres.Add(existingGenre); // Жанр есть -> привязываем
            }
            else
            {
                newBook.Genres.Add(new Genres { Name = genreName }); // Жанра нет -> создаем
            }
        }

        // 3. Сохраняем в базу
        await _context.Books.AddAsync(newBook, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newBook.Id;
    }
    
    
    //добоаляем при создание контролеров 
    public async Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken)
    {
        // 1. Ищем книгу в базе по ID
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        // 2. Если книги нет - возвращаем false (нечего удалять)
        if (book == null)
        {
            return false;
        }

        // 3. Если нашли - удаляем и сохраняем изменения
        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    
}