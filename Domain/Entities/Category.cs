

using Domain.Common;
using Domain.Events;

namespace Domain.Entities
{
    public class Category : AggregateRoot
    {
        public string Name { get; private set; }
        
        // Navigation Property
        public List<Product> Products { get; set; } = new List<Product>();

        private Category(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public static Category Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be null or empty", nameof(name));

            if (name.Length < 2)
                throw new ArgumentException("Category name must be at least 2 characters", nameof(name));

            if (name.Length > 50)
                throw new ArgumentException("Category name cannot exceed 50 characters", nameof(name));

            var categoryId = Guid.NewGuid();
            var category = new Category(categoryId, name.Trim());
            
            category.AddDomainEvent(new CategoryCreatedEvent(categoryId, name.Trim()));

            return category;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Category name cannot be null or empty", nameof(newName));

            if (newName.Length < 2)
                throw new ArgumentException("Category name must be at least 2 characters", nameof(newName));

            if (newName.Length > 50)
                throw new ArgumentException("Category name cannot exceed 50 characters", nameof(newName));

            Name = newName.Trim();
        }

        public bool HasProducts()
        {
            return Products.Any();
        }

        // EF Core constructor
        private Category() : base(Guid.NewGuid()) { }
    }
}