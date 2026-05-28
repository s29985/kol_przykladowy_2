using WebApplication4.DTO;

namespace WebApplication4.Services;

public interface IDbService
{
    // Метод GET (получение)
    Task<List<BookResponse>> GetBooksAsync(string? title, CancellationToken cancellationToken);
    
    // Метод POST (добавление) - возвращает ID созданной книги
    Task<int> AddBookAsync(BooksRequest request, CancellationToken cancellationToken);
    
    //добавляем при создание контролеров 
    // Возвращает true, если удалили, и false, если не нашли
    Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken);
}