namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Foreign Key
        public Guid CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }
    }
}

