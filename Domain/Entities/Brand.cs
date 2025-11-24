using Domain.Common;
using Domain.Events;

namespace Domain.Entities
{
    public class Brand : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Brand(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public static Brand Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Brand name cannot be empty", nameof(name));

            var id = Guid.NewGuid();
            var brand = new Brand(id, name, description);
            
            // You might want to add a domain event here like BrandCreatedEvent
            // brand.AddDomainEvent(new BrandCreatedEvent(id, name));

            return brand;
        }

        public void Update(string name, string description)
        {
             if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Brand name cannot be empty", nameof(name));

            Name = name;
            Description = description;
        }

        // EF Core constructor
        private Brand() : base(Guid.NewGuid()) 
        {
            Name = null!;
            Description = null!;
        }
    }
}
