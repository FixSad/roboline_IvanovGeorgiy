Язык программирования: C# + ASP.NET Core.
Версия .net 8
База данных: SqLite

Структура проекта(паттерн):
1) DAL - Слой доступа к данным
2) DOMAIN - Доменный слой, совокупность сущностей и данных
3) SERVICE - Слой бизнес-логики

Требования(общие, дополнительные):
1) Entity Framework Core ✅
2) RESTful API (Также проверил с помощью postman. Все работает отлично) ✅
3) Обработка возможных ошибок ✅
4) Валидация данных ✅
5) Добавлены Логи ✅
6) Добавлены миграции ✅

Инструкция по запуску:
Открыть папку ProductCategory, где лежит sln файл. И в командной строке/powershell написать следующую команду:
dotnet publish ProductCategory.sln -r win-x64 -p:PublishSingleFile=true --self-contained true
Команда создает один файл — ProductCategory.exe, который находится в 
ProductCategory\bin\Release\net8.0\win-x64\publish\ProductCategory.exe

Сам не смог залить exe файл. Потому что, github не дает залить файл больше 100мг.)
