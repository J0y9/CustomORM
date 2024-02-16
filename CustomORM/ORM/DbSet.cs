using System.Collections;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CustomORM.ORM;

public class DbSet<T> : List<T>
{
    // private List<T> Entities { get; set; }
    
    public DbSet()
    {
        // Entities = entity;
    }

    public void AddRange()
    {
        
    }
    public void Add(T entity)
    {
        string tableName = nameof(entity) + "s";
        var type = typeof(T);
        var properties = type.GetProperties();
        string columnsNames = string.Join(",", properties.Select(x => x.Name));
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
        ExecuteNonQuery(query);

    }

    public List<T> Get()
    {
        var tableName = nameof(T) + "s";
        return ExecuteQuery($"select * from {tableName}");
        // select * from tableName
    }

    public void Update(T entity)
    {
        // Update tableName Set id=2,name='a' where id = entity.Id \
        var tableName = nameof(T) + "s";
        var properties = typeof(T).GetProperties();
       var entityId = properties.Single(x => x.Name == "Id").GetValue(entity);
        string columnsNamesAndValues = string.Join(',', properties.Select(x => $"{x.Name}={x.GetValue(entity)}"));
        ExecuteNonQuery($"UPDATE  {tableName} {columnsNamesAndValues} WHERE id={entityId}");
        
        // TODO: Update single or multiple column 

    }

    public void Delete(T entity) 
    {
        var tableName = nameof(T) + "s";
        var properties = typeof(T).GetProperties();
        var entityId = properties.Single(x => x.Name == "Id").GetValue(entity);
        ExecuteNonQuery($"DELETE FROM {tableName} WHERE id={entityId}");

    }
    public void ExecuteNonQuery(string query)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString))
            {
                sqlConnection.Open();
                
                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                // Q/ why should i close here isn't the using do it by it self
            }
        }

    public List<T> ExecuteQuery(string query)
        {
            List<T> rows = new List<T>();

            using (var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString))
            {
                
                
                var sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    T entity = Activator.CreateInstance<T>();
                    var properties = entity.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        property.SetValue(entity, Convert.ChangeType(reader[property.Name], property.PropertyType));
                    }
                    rows.Add(entity);

                }
                

            }

            return rows;
        }
    
    
}