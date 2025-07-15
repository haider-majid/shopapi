using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IAccountRepository AccountRepository { get; }
        Task<int> SaveChangesAsync();
    }
}