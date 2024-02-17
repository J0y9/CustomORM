using System.Data;
using System.Data.SqlClient;
using CustomORM.Models;

namespace CustomORM.ORM;

public class DbContext 
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    // database creation
    public void CreateDatabase()
    {
            string dbName = ConnectionString.GetDbName();
            string query = $"IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + 'DBName' + ']' = '{dbName}' OR name = '{dbName}'))) BEGIN  CREATE DATABASE {dbName} PRINT 'DATABASE_CREATED' END ELSE PRINT 'DATABASE_EXIST'";
            using SqlConnection connection = new SqlConnection(ConnectionString.MasterConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query,connection);
            command.ExecuteNonQuery();
            connection.Close();
    }
    
    // tables creation
    public void CreateTables()
    {
        var dbSets = typeof(DbContext).GetProperties();
        var tables = dbSets.Select(x => x.Name).ToList();

        foreach (var table in tables)
        {
                var type = dbSets.First(p => p.Name == table);
                var typeProperties = type.PropertyType.GenericTypeArguments.First().GetProperties();
                var result = new List<string>();
                foreach (var typeProperty in typeProperties)
                {
                    var propertyType = typeProperty.PropertyType;
                    var propertyName = typeProperty.Name;
                    var sqlIdentity = "";
                    var sqlType = "[nvarchar](max)";

                    if (propertyType == typeof(Guid))
                    {
                        sqlType = "uniqueidentifier";
                    }

                    if (propertyType == typeof(decimal))
                    {
                        sqlType = "[decimal](18,0)";
                    }
                    // get property Name and values
                    // [customer_id] [int] IDENTITY(1,1) NOT NULL
                    result.Add($"[{propertyName}] {sqlType} NOT NULL ");

                }
                var columns = string.Join(",", result.Select(s => s));
                var query = $"CREATE TABLE {table} ({columns})";
                using SqlConnection connection = new SqlConnection(ConnectionString.MyConnectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query,connection);
                command.ExecuteNonQuery();
                connection.Close();
            
            // Create Table
        }
        
        
    }
    public DbContext()
    {
       
    }
}
