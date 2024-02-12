using System.Data.SqlClient;
using CustomORM.Models;

namespace CustomORM.ORM;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    // CRUD operations on database
    public void Add<T>(T entity)
    {
        string tableName = nameof(entity) + "s";
        var type = typeof(T);
        var properties = type.GetProperties();
        string columnsNames = string.Join(',', properties.ToString());
        string columnsValues = string.Join(',', properties.Select(
            x =>
            {
                if (x.GetValue(entity) != null)
                {
                    if (x.GetValue(entity).GetType() == typeof(string))
                    {
                        return $"'{x.GetValue(entity)}'";
                    }
                    else
                    {
                        return x.GetValue(entity);
                    }
                    
                }
                return null;
            }
            ));
        string query = $"insert into {tableName} {columnsNames} values {columnsValues}";
        ExecuteQuery(query);

        // foreach (var field in type.GetProperties())
        // {
        //     Console.WriteLine(field.Name);
        // }
        // add column value to table
        // insert into tableName (id, name, price) values (1, "mouse", 99)

    }

    // public IEnumerable<T> Get<T>()
    // {
    //     
    // }
        public void ExecuteQuery(string query)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                // Q/ why should i close here isn't the using do it by it self
            }
        }
}