namespace CustomORM.Models;

public class Item
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = "";
    public decimal Price { get; set; }

    public Item(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }

    public Item()
    {
        
    }
}