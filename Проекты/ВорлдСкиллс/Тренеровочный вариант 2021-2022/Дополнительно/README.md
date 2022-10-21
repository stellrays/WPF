# Задание
Создать решение в котором будут следующие проекты ( Visual Studio 2022, .NET Core 6 )

1. ConsoleApp + EF(Entity Framework Core)
2. ConsoleApp + ADO.net
3. WPF + EF(Entity Framework Core)
4. WPF + ADO.Net
5. Windows Forms + EF(Entity Framework Core)
6. Windows Forms + ADO.Net
7. Windows Forms + ADO.Net (Visual Style)

Каждый проект выводит в DataGrid данные таблицы Users, в которой есть три поля: Id, Name, Age. Консольные приложения выводят данные в консоль.
То есть все 7 проектов делают одно и то же, только на разных технологиях создания интерфейса и работы с базой данных.
База данных: SQL Server ( localdb ). Можно SQLite.

Решение загружаете на github. В репозитории должен быть обязательно README.md с описанием по каждому проекту.
Также должен присутствовать файл .gitignore


## Задание 1
Полезная ссылка https://habr.com/ru/post/694086/

1) Создание Консольного приложения (Microsoft) 

![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/ВорлдСкиллс/Тренеровочный%20вариант%202021-2022/Дополнительно/CreateFile.png?raw=true)

2) Установка EF Core в свой проект

  Установить все нужные библиотеки можно консоль:
  ``` bash
  dotnet add ChapterZero package Microsoft.EntityFrameworkCore.Design
  dotnet add ChapterZero package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add ChapterZero package Microsoft.EntityFrameworkCore
  ```
  
  Или в Visual Studio через NuGet Package Manager. Процесс показан ниже:
  
  a) Переходим в Tools / NuGet Package Manager / Manage NuGet Packages for Solution.

  ![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/ВорлдСкиллс/Тренеровочный%20вариант%202021-2022/Дополнительно/tools.png?raw=true)
  
  b) Переходим в Browse(Обзор) и ищем Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer и Microsoft.EntityFrameworkCore.Design
     Устанавливаем их
     
     ![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/ВорлдСкиллс/Тренеровочный%20вариант%202021-2022/Дополнительно/EFTools.png?raw=true)
