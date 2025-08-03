using System;
using System.Collections.Generic;

namespace storeapi.TempModels;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
