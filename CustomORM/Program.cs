
using System.Reflection;
using CustomORM.Models;

var entity = new Item("item", 999);
var type = typeof(Item);

var properties = type.GetProperties();
string columnsValues = string.Join(',', properties.Select(x=> $"'{x.GetValue(entity)}'"));
Console.WriteLine(columnsValues);
Console.WriteLine(type.GetProperties().Length);
