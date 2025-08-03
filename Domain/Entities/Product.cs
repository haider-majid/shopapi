using Domain.Common;
using Domain.ValueObjects;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Product : AggregateRoot
    {
        public ProductName Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public int Stock { get; private set; }
        public Guid CategoryId { get; private set; }

        // Navigation Property
        public Category? Category { get; set; }

        private Product(Guid id, ProductName name, string description, Money price, int stock, Guid categoryId) 
            : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public static Product Create(ProductName name, string description, Money price, int initialStock, Guid categoryId)
        {
            if (initialStock < 0)
                throw new InsufficientStockException("Initial stock cannot be negative");

            var productId = Guid.NewGuid();
            var product = new Product(productId, name, description, price, initialStock, categoryId);
            
            product.AddDomainEvent(new ProductCreatedEvent(
                productId, 
                name.Value, 
                price.Amount, 
                initialStock, 
                categoryId));

            return product;
        }

        public void UpdatePrice(Money newPrice)
        {
            var oldPrice = Price.Amount;
            Price = newPrice;
            
            AddDomainEvent(new ProductPriceChangedEvent(Id, oldPrice, newPrice.Amount));
        }

        public void AdjustStock(int quantity, string reason)
        {
            var oldStock = Stock;
            var newStock = Stock + quantity;
            
            if (newStock < 0)
                throw new InsufficientStockException($"Cannot reduce stock below zero. Current stock: {Stock}, attempted adjustment: {quantity}");

            Stock = newStock;
            
            AddDomainEvent(new ProductStockChangedEvent(Id, oldStock, newStock, reason));
        }

        public void ReduceStock(int quantity)
        {
            AdjustStock(-quantity, "Stock reduction");
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive", nameof(quantity));
                
            AdjustStock(quantity, "Stock increase");
        }

        public bool IsInStock(int requestedQuantity = 1)
        {
            return Stock >= requestedQuantity;
        }

        public void UpdateDetails(ProductName name, string description)
        {
            Name = name;
            Description = description;
        }

        // EF Core constructor
        private Product() : base(Guid.NewGuid()) { }
    }
}

