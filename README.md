
1. 
dotnet new gitignore

git init
git add .
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/s29985/s29985_Kol_prz.git
git push -u origin main



2.
# 2. ТУТ ПИШЕМ КОД:
# - Entities (Модели)
# - DbContext
# - В appsettings.json: "DefaultConnection": "Data Source=ExamDb.db"
# - В Program.cs: builder.Services.AddDbContext<...>(options => options.UseSqlite(...))

# 3. Создаем слепок базы данных (миграцию)
dotnet ef migrations add InitSqlite

# 4. Применяем изменения (создастся файл ExamDb.db)
dotnet ef database update

