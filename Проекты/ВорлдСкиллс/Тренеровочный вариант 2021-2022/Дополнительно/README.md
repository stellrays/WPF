# Задание
Создать решение в котором будут следующие проекты ( Visual Studio 2022, .NET Core 6 )

1. ConsoleApp + EF(Entity Framework Core)    (Microsoft) 
2. ConsoleApp + ADO.net                      (Microsoft) 
3. WPF + EF(Entity Framework Core)           (Microsoft) 
4. WPF + ADO.Net                             (Microsoft) 
5. Windows Forms + EF(Entity Framework Core) (Microsoft) 
6. Windows Forms + ADO.Net                   (Microsoft) 
7. Windows Forms + ADO.Net (Visual Style)    (.Net Framework) 

Каждый проект выводит в DataGrid данные таблицы Users, в которой есть три поля: Id, Name, Age. Консольные приложения выводят данные в консоль.
То есть все 7 проектов делают одно и то же, только на разных технологиях создания интерфейса и работы с базой данных.
База данных: SQL Server ( localdb ). Можно SQLite.

Решение загружаете на github. В репозитории должен быть обязательно README.md с описанием по каждому проекту.
Также должен присутствовать файл .gitignore

### Скрип SQL создания таблицы Users

``` bash
CREATE TABLE [dbo].[Users](
	[Id] [int] NOT NULL,
	[Name] [varchar](100) NULL,
	[Age] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
    ( [Id] ASC ) WITH 
        (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
```
<code>

## Этап 1
	
1) Создание Консольного приложения (Microsoft + Entity Framework Core)

   ![alt text]()
   
## Этап 2

⋅⋅⋅1) Создание Консольного приложения (Microsoft + ADO.net)

## Этап 3

⋅⋅⋅1) Создание WPF приложения (Microsoft + Entity Framework Core)

## Этап 4

1) Создание WPF приложения (Microsoft + ADO.net)

## Этап 5

1) Создание Windows Forms приложения (Microsoft + Entity Framework Core)

## Этап 6

1) Создание Windows Forms приложения (Microsoft + ADO.net)

## Этап 7

1) Создание Windows Forms приложения (.Net Framework + ADO.net)
</code>
