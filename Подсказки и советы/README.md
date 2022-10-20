# Файл конфигурации приложения App.config

``` bash
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
 <connectionStrings>
 <add 
	name="DefaultConnection"
	connectionString="Server=localhost,63027;Database=UserDatabase;Trusted_Connection=True"
	providerName="System.Data.SqlClient"/>

 <add
	name="ConnectionLocalDb"
	connectionString="Server=(localdb)\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;"
	providerName="System.Data.SqlClient"/>

 <add 
 	name="
	ConnectionSQLite"connectionString="Data Source=FabricShop.db"
	providerName="System.Data.SQLite" />

 </connectionStrings>
</configuration>
```
# Подключение базы данных для контекста данных EF
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
