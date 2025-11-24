using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Common;
using Application.Common;
using Domain.ValueObjects;

namespace Infrastructure.Data
{
    public class ShopDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public ShopDbContext(DbContextOptions<ShopDbContext> options, IDomainEventDispatcher domainEventDispatcher)
            : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                // Value Object mappings
                entity.OwnsOne(e => e.Name, name =>
                {
                    name.Property(n => n.Value)
                        .HasColumnName("Name")
                        .IsRequired()
                        .HasMaxLength(100);
                });

                entity.OwnsOne(e => e.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();
                    price.Property(p => p.Currency)
                        .HasColumnName("Currency")
                        .HasMaxLength(3)
                        .IsRequired();
                });

                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.CategoryId).IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Ignore(e => e.DomainEvents);
            });

            // Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Ignore(e => e.DomainEvents);
            });

            // Brand configuration
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Description)
                    .HasMaxLength(250);

                entity.Ignore(e => e.DomainEvents);
            });


        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entitiesWithEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entity in entitiesWithEvents)
            {
                await _domainEventDispatcher.DispatchAsync(entity.DomainEvents);
                entity.ClearDomainEvents();
            }

            return result;
        }
    }
}