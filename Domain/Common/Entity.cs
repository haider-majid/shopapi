namespace Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty", nameof(id));
            
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }
    }
}