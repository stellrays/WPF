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

## Этап 1 (в процессе)
	
   1) Создание Консольного приложения (Microsoft + Entity Framework Core)

   ![alt text]()
   
## Этап 2 (в процессе  я тут)

   1) Создание Консольного приложения (Microsoft + ADO.net)

## Этап 3 (в процессе)

   1) Создание WPF приложения (Microsoft + Entity Framework Core)
   2) Подключим необходимый пакет EntityFramework, Microsoft.EntityFrameworkCore.SqlServer
   3) В новой папке Models создать классы User и UserContext
   
   Код класса User
   ``` bash
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	namespace WPFEntityFrameworkCore.Models
	{
	    public class User
	    {
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public int Age { get; set; }
	    }
	}

   ```
   Для взаимодействия с базой данных через Entity Framework нам нужен контекст данных
   Код класса UserContext
   ``` bash
   
   ```
   Замечание: User - это класс модели, Users - это название таблицы в базе данных Класс контекста наследуется от класса DbContext. В своем конструкторе он          передает в конструктор базового класса название строки подключения из файла App.config. Также в контексте данных определяется свойство по типу DbSet<Product> -        через него мы будем взаимодействовать с таблицей, которая хранит объекты Product.

   В разметки Xaml
   ``` bash
	<DataGrid AutoGenerateColumns="False" x:Name="usersGrid">
            <DataGrid.Columns>
                <DataGridTextColumn Binding="{Binding Id}" Header="ID" Width="50"/>
                <DataGridTextColumn Binding="{Binding Name}" Header="Имя" Width="120"/>
                <DataGridTextColumn Binding="{Binding Age}" Header="Возраст" Width="80"/>
            </DataGrid.Columns>
        </DataGrid>
   ```

Замечание: при таком подходе надо изначально создавать базу данных на сервере или в классе AppContext прописать создание базы данных автоматически.
## Этап 4 (в процессе)

   1) Создание WPF приложения (Microsoft + ADO.net)

## Этап 5 (в процессе)

   1) Создание Windows Forms приложения (Microsoft + Entity Framework Core)
   2) Добавить  пакет EntityFramework
   3) В новой папке Models создать классы User и UserContext
	
	 Код класса User:
	   ``` bash
		using System;
		using System.Collections.Generic;
		using System.Linq;
		using System.Text;
		using System.Threading.Tasks;

		namespace WinFormsAppEntityFrameworkCore.Models
		{
		    internal class User
		    {
			public int Id { get; set; }
			public string Name { get; set; } = null!;
			public int Age { get; set; }
		    }
		}
	   ```
	 Для взаимодействия с базой данных через Entity Framework нам нужен контекст данных
	
	   Код класса UserContext:
	   ``` bash
		using System.Data.Entity;

		namespace WinFormsAppEntityFrameworkCore.Models
		{
		    internal class UserContext : DbContext
		    {
			public UserContext() : base("DefaultConnection") { }
			public DbSet<User> Users { get; set; }
		    }
		}
	   ```
	
	Свойства грида: AllowsUserToAddRows = False
			SelectionMode = FullColumnSelect
## Этап 6 (в процессе)

   1) Создание Windows Forms приложения (Microsoft + ADO.net)
   2) Для работы с базой данных MS SQL Server в .NET 5 и выше (а также .NET Core 3.0/3.1) добавить пакет Microsoft.Data.SqlClient
   3) 
	

## Этап 7

   1) Создание Windows Forms приложения (.Net Framework + ADO.net)
   2) Добавив DataGridView на форму, добавить источник данных: 
	
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/ВорлдСкиллс/Тренеровочный%20вариант%202021-2022/Дополнительно/Screen/DataSource.png?raw=true)
	
   3) Выбрав источник данных и тип модели базы данных, добавить подключение:
	
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/ВорлдСкиллс/Тренеровочный%20вариант%202021-2022/Дополнительно/Screen/DataSource1.png?raw=true)

   4) Выбрать объекты базы данных, которые хотим вывести:
   
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/ВорлдСкиллс/Тренеровочный%20вариант%202021-2022/Дополнительно/Screen/DataSource2.png?raw=true)

   5) После запуска приложения получить следующий результат:
   
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/ВорлдСкиллс/Тренеровочный%20вариант%202021-2022/Дополнительно/Screen/DataSourceRezult.png?raw=true)
