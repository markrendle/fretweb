namespace FretWeb.Models;

public class NavigationViewModel
{
    public List<Row> Rows { get; } = new();
    
    public class Item
    {
        public string Display { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class Row
    {
        public string Name { get; set; }
        public List<Item> Buttons { get; } = new();
        public string Id { get; set; }
    }
}