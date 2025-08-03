using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new InvalidMoneyException("Amount cannot be negative");

            if (string.IsNullOrWhiteSpace(currency))
                throw new InvalidMoneyException("Currency cannot be null or empty");

            Amount = Math.Round(amount, 2);
            Currency = currency.ToUpperInvariant();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidMoneyException("Cannot add money with different currencies");

            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidMoneyException("Cannot subtract money with different currencies");

            return new Money(left.Amount - right.Amount, left.Currency);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            return new Money(money.Amount * multiplier, money.Currency);
        }

        public static implicit operator decimal(Money money) => money.Amount;

        public override string ToString() => $"{Amount:C} {Currency}";
    }
}