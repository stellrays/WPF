## Файл конфигурации приложения App.config

``` bash
<?xml version="1.0" encoding="utf-8" ?>
<configuration>

 <connectionStrings>
 
 <add name="DefaultConnection"
	connectionString="Server=localhost,63027;Database=UserDatabase;Trusted_Connection=True"
	providerName="System.Data.SqlClient"/>

 <add name="ConnectionLocalDb"
	connectionString="Server=(localdb)\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;"
	providerName="System.Data.SqlClient"/>

 <add name="ConnectionSQLite"
 	connectionString="Data Source=FabricShop.db"
	providerName="System.Data.SQLite" />

 </connectionStrings>
 
</configuration>
```

## Подключение базы данных для контекста данных EF

``` bash
protected override (DbContextOptionsBuilder optionsBuilder)
{ 
	if (!optionsBuilder.IsConfigured)
	{
		// SQL Server connection with port
			 //optionsBuilder.UseSqlServer("Server=localhost,63027;Database=UserDatabase;Trusted_Connection=True;");

		// SQL Server connection with localdb
			//optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;");

		// SQL Server connection from App.config
			//optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
			//optionsBuilder.UseSqlServer(ConfigurationManager.["ConnectionLocalDb"].ToString());

		// SQlite connection 
			//optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["ConnectionSQLite"].ToString());
			//optionsBuilder.UseSqlite(@"DataSource=ColledgeStore.db;");

	}
}
```
## Взаимодействие с базой данных SQL Server через ADO.NET

Хранимая процедура, которая осуществляет добавление нового объекта в базу данных.

``` bash
CREATE PROCEDURE [dbo].[sp_InsertPhone]
    @title nvarchar(50),
    @company nvarchar(50),
    @price int,
    @Id int out
AS
    INSERT INTO Phones (Title, Company, Price)
    VALUES (@title, @company, @price)
   
    SET @Id=SCOPE_IDENTITY()
GO
```
Атрибут connectionString собственно хранит строку подключения. Он состоит из трех частей:

* Data Source=localhost: указывает на название сервера. По умолчанию для MS SQL Server Express используется "localhost"
* Initial Catalog=mobiledb: название базы данных.
* Integrated Security=True: задает режим аутентификации

Вывод данных в DataGrid. AutoGenerateColumns="False" позволяет делать привязку к нужным столбцам.

``` bash
<DataGrid 
 	AutoGenerateColumns="False"
	x:Name="phonesGrid">
            <DataGrid.Columns>
                <DataGridTextColumn Binding="{Binding Title}" Header="Модель" Width="120"/>
                <DataGridTextColumn Binding="{Binding Company}" Header="Производитель" Width="125"/>
                <DataGridTextColumn Binding="{Binding Price}" Header="Цена" Width="80"/>
            </DataGrid.Columns>
</DataGrid>
```

Вся работа с бд производится стандартными средствами ADO.NET и прежде всего классом SqlDataAdapter. Вначале мы получаем в конструкторе строку подключения, которая определена выше в файле app.config:

``` bash
connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
```

Чтобы задействовать эту функциональность, нам надо добавить в проект библиотеку System.Configuration.dll.

Далее в обработчике загрузки окна Window_Loaded создаем объект SqlDataAdapter:

``` bash
adapter = new SqlDataAdapter(command);
```

В качестве команды для добавления объекта устанавливаем ссылку на хранимую процедуру:
``` bash
adapter.InsertCommand = new SqlCommand("sp_InsertPhone", connection);
```

Получаем данные из БД и осуществляем привязку:
``` bash
adapter.Fill(phonesTable);
phonesGrid.ItemsSource = phonesTable.DefaultView;
```

За обновление отвечает метод UpdateDB():
``` bash
private void UpdateDB()
{
    SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
    adapter.Update(phonesTable);
}
```

Чтобы обновить данные через SqlDataAdapter, нам нужна команда обновления, которую можно получить с помощью объекта SqlCommandBuilder. Для самого обновления вызывается метод adapter.Update().

``` bash
Причем не важно, что мы делаем в программе - добавляем, редактируем или удаляем строки. Метод adapter.Update сделает все необходимые действия. Дело в том, что при загрузке данных в объект DataTable система отслеживает состояние загруженных строк. В методе adapter.Update() состояние строк используется для генерации нужных выражений языка SQL, чтобы выполнить обновление базы данных. В обработчике кнопки обновления просто вызывается этот метод UpdateDB, а в обработчике кнопки удаления предварительно удаляются все выделенные строки.
Таким образом, мы можем вводить в DataGrid новые данные, редактировать там же уже существующие, сделать множество изменений, и после этого нажать на кнопку обновления, и все эти изменения синхронизируются с базой данных.
Причем важно отметить действие хранимой процедуры - при добавлении нового объекта данные уходят на сервер, и процедура возвращает нам id добавленной записи. Этот id играет большую роль при генерации нужного sql-выражения, если мы захотим эту запись изменить или удалить. И если бы не хранимая процедура, то нам пришлось бы после добавления данных загружать заново всю таблицу в datagrid, только чтобы у новой добавленной записи был в datagrid id. И хранимая процедура избавляет нас от этой работы.
```

``` bash
private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM users";
            usersTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                //установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertUsers", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 10, "name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@age", SqlDbType.Int, 10, "age"));
                
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(usersTable);
                usersGrid.ItemsSource = usersTable.DefaultView;  // Заметь, что не DataSource, а ItemSource, чтобы Binding работал в xaml
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
```

``` bash
 private void UpdateDB()
        {
            SqlCommandBuilder commandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(usersTable);
            MessageBox.Show("Данные обновлены");
        }
```

``` bash
private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();       
        }        
```
Метод удаления
``` bash

private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (usersGrid.SelectedItems != null)
            {
                for (int i = 0; i < usersGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = usersGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }
```
