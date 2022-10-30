# Задание (надо сделать содержание ссылками)
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

``` sql
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

### Код файла App.config, используемый в ниже описанных проектах:

``` xml
	 <?xml version="1.0" encoding="utf-8" ?>
	 <configuration>
	 <connectionStrings>
		 <add  name="ConnectionLocalDb"
		       connectionString="Server = DESKTOP-0VRO2QB\SQLEXPRESS_2;Database=UserDatabase;Trusted_Connection=True;TrustServerCertificate=True;"
		       providerName="System.Data.SqlClient"/>
	 </connectionStrings>
	 </configuration>
```

## Этап 1
	
   1) Создать Консольного приложения (Microsoft + Entity Framework Core)
   2) Подключить необходимые пакеты Microsoft.EntityFrameworkCore.SqlServer и Microsoft.EntityFrameworkCore.Tools
   3) В новой папке Models создать классы User и UserContext
   
   Код класса User:
   
   ``` Csharp
   using System;
   using System.Collections.Generic;
   namespace ConsoleAppEntityFrameworkCore.Model
   {
    public class User
	    {
		public int Id { get; set; }
		public string Name { get; set; } = null;
		public int Age { get; set; }
	    }
   }

   ```
   Код контекстного класса UserContext:
   
   
   ``` Csharp
   using System;
   using System.Collections.Generic;
   using Microsoft.EntityFrameworkCore;
   using Microsoft.EntityFrameworkCore.Metadata;
   using Microsoft.IdentityModel.Protocols;
   namespace ConsoleAppEntityFrameworkCore.Model
   {
    public partial class UserContext : DbContext
    {
        public UserContext()
        {
        }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server = DESKTOP-0VRO2QB\SQLEXPRESS_2;Database=UserDatabase;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
   }

   ```
   4) Код для Program.cs:
   
   ``` Csharp
   using System.Reflection.PortableExecutable;
   using ConsoleAppEntityFrameworkCore.Model;
   using static ConsoleAppEntityFrameworkCore.Model.User;
   using (UserContext db = new UserContext())
   {
    // получаем объекты из бд и выводим на консоль
    var users = db.Users.ToList();
    Console.WriteLine("Вывод данных:");
    foreach (User u in users)
    {        
        Console.WriteLine($"ID\t Name\t Age\n" +
            $"{u.Id}\t {u.Name}\t {u.Age}");
    }
   }
   ```
   5) Результат проекта:
   
   ![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/ConsoleAppEntityFrameworkCoreRezult.png?raw=true)
   
## Этап 2

   1) Созданть Консольного приложения (Microsoft + ADO.net)
   2) Для работы с базой данных MS SQL Server в .NET 5 и выше (а также .NET Core 3.0/3.1) добавить пакет Microsoft.Data.SqlClient
   3) Итоговый файл Program.cs выглядит следующим образом:
   ``` Csharp
	using Microsoft.Data.SqlClient;
	using System.Data;
	string connectionString = "Server=DESKTOP-0VRO2QB\\SQLEXPRESS_2;Database=UserDatabase;Trusted_Connection=True; TrustServerCertificate=True;";
	string sqlExpression = "SELECT * FROM Users";
	Console.WriteLine("Подключение открыто");
	using (SqlConnection connection = new SqlConnection(connectionString))
	{
	    await connection.OpenAsync();

	    SqlCommand command = new SqlCommand(sqlExpression, connection);
	    SqlDataReader reader = await command.ExecuteReaderAsync();

	    if (reader.HasRows) // если есть данные
	    {
		// выводим названия столбцов
		string columnName1 = reader.GetName(0);
		string columnName2 = reader.GetName(1);
		string columnName3 = reader.GetName(2);

		Console.WriteLine($"{columnName1}\t{columnName3}\t{columnName2}");

		while (await reader.ReadAsync()) // построчно считываем данные
		{
		    object id = reader.GetValue(0);
		    object name = reader.GetValue(2);
		    object age = reader.GetValue(1);

		    Console.WriteLine($"{id} \t{name} \t{age}");
		}
	    }

	    await reader.CloseAsync();
	    Console.WriteLine("Подключение закрыто");
	}
	Console.Read();
   ```
   4) Результат проекта:
   
   ![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/ConsoleAppADOnetRezult.png?raw=true)
   
   ``` bash

    Примечание: в строку подключения нужно было дописать команду TrustServerCertificate=True;
    
   ```
   

## Этап 3

   1) Создать WPF приложения (Microsoft + Entity Framework Core)
   2) Подключить необходимый пакет Microsoft.EntityFrameworkCore.SqlServer + Microsoft.EntityFrameworkCore.Tools
   3) В новой папке Models создать классы User и UserContext
   
   Код класса User
 
   ``` Csharp
	namespace WPFEntityFrameworkCore.Model
	{
	    public class User
	    {
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Age { get; set; }
	    }
	}
   ```
   
     Код класса UserContext
 
   ``` Csharp
	using Microsoft.EntityFrameworkCore;
	using System.Configuration;
	namespace WPFEntityFrameworkCore.Model
	{
	    public partial class UserContext : DbContext
	    {
		public UserContext()
		{
		}
		public UserContext(DbContextOptions<UserContext> options)
		    : base(options) { }        
		public DbSet<User> Users => Set<User>();
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		    if (!optionsBuilder.IsConfigured)
		    {
			// Подключение к SQL Server из App.config
			optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ToString());
		    }
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		    OnModelCreatingPartial(modelBuilder);
		}
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	    }
	}
   ```
   
   ``` bash
   
	Замечание: User - это класс модели, Users - это название таблицы в базе данных Класс контекста наследуется от класса DbContext. 
	В своем конструкторе он передает в конструктор базового класса название строки подключения из файла App.config. Также в контексте 
	данных определяется свойство по типу DbSet<User> - через него мы будем взаимодействовать с таблицей, которая хранит объекты User.
   ```
  
   4) Добавить разметку

   В разметки Xaml
   ``` xml
	<DataGrid AutoGenerateColumns="False" x:Name="UsersGrid">
            <DataGrid.Columns>
                <DataGridTextColumn Binding="{Binding Id}" Header="ID" Width="50"/>
                <DataGridTextColumn Binding="{Binding Name}" Header="Имя" Width="120"/>
                <DataGridTextColumn Binding="{Binding Age}" Header="Возраст" Width="80"/>
            </DataGrid.Columns>
        </DataGrid>
   ```
   
   5) Описать класс MainWindow.xaml.cs
   
   ``` Csharp
   	using System.Windows;
	using Microsoft.EntityFrameworkCore;
	using WPFEntityFrameworkCore.Model;
	namespace WPFEntityFrameworkCore
	{
	    public partial class MainWindow : Window
	    {
		public MainWindow()
		{
		    InitializeComponent();
		    UserContext db;
		    db = new UserContext();
		    db.Users.Load();
		    UsersGrid.ItemsSource = db.Users.Local.ToBindingList();
		}
	    }
	}
   ``` 
   
   6) Результат выполнения кода:
   
   ![alt text]()
Замечание: при таком подходе надо изначально создавать базу данных на сервере или в классе AppContext прописать создание базы данных автоматически.

## Этап 4

   1) Создать WPF приложения (Microsoft + ADO.net)
   2) Добавить пакет Microsoft.EntityFrameworkCore.SqlServer
   3) Добавить необходимые элементы на форму
   
   Разметка MainWindow.xaml
   ``` xml
	<Grid  ShowGridLines="True">
        	   <Grid.RowDefinitions>
           	   <RowDefinition Height="*" />
            	   <RowDefinition Height="Auto" />
     	     </Grid.RowDefinitions>
     	     <DataGrid AutoGenerateColumns="False" x:Name="UsersGrid">
		    <DataGrid.Columns>
			<DataGridTextColumn Binding="{Binding Id}" Header="ID" Width="100"/>
			<DataGridTextColumn Binding="{Binding Name}" Header="Name" Width="110"/>
			<DataGridTextColumn Binding="{Binding Age}" Header="Age" Width="70"/>
		    </DataGrid.Columns>
              </DataGrid>
    	 </Grid>
   ```
   
   4) Добавить App.config
   ссылка
   5) Описать класс MainWindow.xaml.cs
   
   ``` Csharp
   	using System.Windows;
	using System.Data;
	using System.Configuration;
	using Microsoft.Data.SqlClient;
	namespace WPFAppADONet
	{
		public partial class MainWindow : Window
		{
			public MainWindow()
			{
			    InitializeComponent();
			    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ConnectionString; ;
			    string sql = "SELECT * FROM users";
			    DataTable usersTable = new DataTable();
			    SqlConnection connection = null;
			    connection = new SqlConnection(connectionString);
			    SqlCommand command = new SqlCommand(sql, connection);
			    SqlDataAdapter adapter = new SqlDataAdapter(command);                      
			    connection.Open();
			    adapter.Fill(usersTable);
			    UsersGrid.ItemsSource = usersTable.DefaultView;
			}
		}
	}	
   ```
   
   6) Результат выполнения кода:
   
   ![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/WPFAppADONetRezult.png?raw=true)

## Этап 5

   1) Создать Windows Forms приложения (Microsoft + Entity Framework Core)
   2) Добавить  пакет Microsoft.EntityFrameworkCore.SqlServer + Microsoft.EntityFrameworkCore.Tools
   3) В новой папке Model создать классы User и UserContext
	
   Код класса User:
   ``` Csharp
	   namespace WinFormsAppEntityFrameworkCore.Model
	   {
    	   public class User
    	      {
    	       public int Id { get; set; }
    	       public string? Name { get; set; }
      	       public int Age { get; set; }
   	      }
	   }
   ```
	   	   
   Код класса UserContext
   ``` Csharp
	using Microsoft.EntityFrameworkCore;
	using System.Configuration;
	using WinFormsAppEntityFrameworkCore.Model;
	public partial class UserContext : DbContext
    	{
       		public UserContext()
        	{
        	}
        	public UserContext(DbContextOptions<UserContext> options)
         	   : base(options)
        	{
        	}
       	 	public DbSet<User> Users => Set<User>();
       		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        	{
           	 if (!optionsBuilder.IsConfigured)
            		{
            		// Подключение к SQL Server из App.config
            		optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ToString());         
            		}
        	}
        	protected override void OnModelCreating(ModelBuilder modelBuilder)
        	{
            	OnModelCreatingPartial(modelBuilder);
        	}
        	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    	}
   ```
   
   
   4) Добавть DataGridView на форму
   5) Дописать код для формы
   
   Код Form1:
   
   ``` Csharp
   	using Microsoft.EntityFrameworkCore;
	namespace WinFormsAppEntityFrameworkCore
	{
   	 public partial class Form1 : Form
   	 {
        	public Form1()
        	{
           	 InitializeComponent();
           	 UserContext db;
           	 db = new UserContext();
           	 db.Users.Load();
           	 dataGridView1.DataSource = db.Users.Local.ToBindingList();
        	}
   	 }
	}
   ```
   
   6) Результат выполнения кода:
   
   ![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/WinFormsAppEntityFrameworkCoreRezult.png?raw=true)
   
## Этап 6

   1) Создать Windows Forms приложения (Microsoft + ADO.net)
   2) Добавить пакет Microsoft.Data.SqlClient и DataGridView
   3) Сделать строку подключения через App.config

	ссылка

	
   4) Код Form1.cs:
   
		``` Csharp
			using Microsoft.Data.SqlClient;
			using System.Configuration;
			using System.Data;
			using System.Data.Common;
			using System.Drawing.Text;
			namespace WinFormsAppADONet
			{
			   public partial class Form1 : Form
			   {     
				   public Form1()
				   {
				    InitializeComponent();
				    LoadData();
				   }
				   private SqlDataAdapter adapter = null;
				   private DataTable dataTable = null;
				   private void LoadData()
				   {            
				       string connectionString = ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ConnectionString;
				       // Создание подключения
				       using (SqlConnection connection = new SqlConnection(connectionString))
				       {
					   connection.Open();
					   //Подключение открыто
					   adapter = new SqlDataAdapter("SELECT * FROM Users", connectionString) ;
					   dataTable = new DataTable();
					   adapter.Fill(dataTable);
					   dataGridView1.DataSource = dataTable;
				       }
				    //Подключение закрыто
				   }
			     }
			  }	
		```
	
   5) Результат проекта:
	
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/WinFormsAppADONetResult.png?raw=true)
	
## Этап 7

   1) Создать Windows Forms приложения (.Net Framework + ADO.net)
   2) Добавив DataGridView на форму, добавить источник данных: 
	
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/DataSource.png?raw=true)
	
   3) Выбрав источник данных и тип модели базы данных, добавить подключение:
	
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/DataSource1.png?raw=true)

   4) Выбрать объекты базы данных, которые хотим вывести:
   
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/DataSource2.png?raw=true)

   5) Результат проекта:
   
![alt text](https://github.com/stellrays/WPF/blob/main/Проекты/WPF_WinForm_and_EF_ADOnet/Screen/DataSourceRezult.png?raw=true)
