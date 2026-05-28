using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTO;
using WebApplication4.Services;

namespace WebApplication4.Controllers;

// Обязательные атрибуты для API
[ApiController]
[Route("api/[controller]")] // URL автоматически станет /api/books
public class BooksController : ControllerBase
{
    private readonly IDbService _service;

    // Внедряем нашего "шеф-повара" (сервис) через конструктор
    public BooksController(IDbService service)
    {
        _service = service;
    }

    // ==========================================
    // 1. GET: Получить список книг 
    // Пример URL: /api/books или /api/books?title=Гарри
    // ==========================================
    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? title, CancellationToken cancellationToken)
    {
        // Просим сервис достать данные из базы
        var books = await _service.GetBooksAsync(title, cancellationToken);
        
        // Возвращаем статус 200 OK и массив данных в формате JSON
        return Ok(books); 
    }

    // ==========================================
    // 2. POST: Добавить новую книгу
    // Пример URL: /api/books (в теле запроса передается JSON с данными)
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] BooksRequest request, CancellationToken cancellationToken)
    {
        // Передаем данные в сервис для сохранения
        var newBookId = await _service.AddBookAsync(request, cancellationToken);
        
        // Возвращаем статус 201 Created. 
        // Указываем путь, по которому можно найти новую книгу, и возвращаем её ID.
        return Created($"api/books/{newBookId}", new { Id = newBookId });
    }

    // ==========================================
    // 3. DELETE: Удалить книгу по ID
    // Пример URL: /api/books/5 (где 5 - это ID книги)
    // ==========================================
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook([FromRoute] int id, CancellationToken cancellationToken)
    {
        // Просим сервис удалить книгу
        var isDeleted = await _service.DeleteBookAsync(id, cancellationToken);

        // Если сервис вернул false, значит книги с таким ID нет в базе
        if (!isDeleted)
        {
            return NotFound(); // Статус 404
        }

        // Если удаление прошло успешно, возвращаем пустой ответ об успехе
        return NoContent(); // Статус 204
    }
}