// === 1. ДОБАВЛЯЕМ USING СВЕРХУ ===
using Microsoft.EntityFrameworkCore;
using WebApplication4.Infrastructure; 
using WebApplication4.Services; // <-- НЕ ЗАБУДЬ ДОБАВИТЬ ЭТОТ USING ДЛЯ СЕРВИСА
// =====================================

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// === 2. РЕГИСТРИРУЕМ БАЗУ ДАННЫХ ===
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// === 3. РЕГИСТРИРУЕМ НАШ СЕРВИС ===
// Добавляем его прямо здесь, под базой данных!
builder.Services.AddScoped<IDbService, DbService>();
// ===================================

// После builder.Build() регистрировать сервисы уже нельзя!
var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();