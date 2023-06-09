namespace FretWeb.Models;

public class NavigationViewModel
{
    public List<Row> Rows { get; } = new();
    
    public class Item
    {
        public required string Display { get; init; }
        public required string Href { get; init; }
        public required string Title { get; init; }
        public required string Text { get; init; }
    }

    public class Row
    {
        public required string Name { get; init; }
        public List<Item> Buttons { get; } = new();
        public required string Id { get; init; }
    }
}