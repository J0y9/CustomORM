
using System.Data;
using System.Reflection;
using CustomORM.Models;
using CustomORM.ORM;

// var entity = new Item("item", 999);
// var type = typeof(Item);
//
// var properties = type.GetProperties();
// string columnsNames = string.Join(",", properties.Select(x => x.Name));
//
// var connectionString = ConnectionString.MyConnectionString;
//
// Console.WriteLine();

DbContext context = new DbContext();
context.CreateDatabase();

