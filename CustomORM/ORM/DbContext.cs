using System.Data;
using System.Data.SqlClient;
using CustomORM.Models;

namespace CustomORM.ORM;

public class DbContext 
{
    // database connection
    
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
    public DbContext()
    {
       
    }
}
