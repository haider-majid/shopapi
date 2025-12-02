using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;
public sealed class ProductName : ValueObject
{
    public string Value { get; private set; }

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidProductNameException("Product name cannot be null or empty");

        if (value.Length > 100)
            throw new InvalidProductNameException("Product name cannot exceed 100 characters");

        if (value.Length < 3)
            throw new InvalidProductNameException("Product name must be at least 3 characters");

        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductName productName) => productName.Value;
    public static explicit operator ProductName(string productName) => new(productName);

    public override string ToString() => Value;
}
