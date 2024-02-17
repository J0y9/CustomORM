
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
//context.CreateTables();

DbSet<Item> Items = new DbSet<Item>();
Item item = new Item("Item", 99);
//Items.Add(item);
foreach (var iteme in Items.Get())
{
    iteme.Name = "Item2";
    iteme.Price = 00;
    Items.Delete(iteme);
}


