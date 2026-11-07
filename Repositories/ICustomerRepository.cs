using KodaStore.Api.Models;

namespace KodaStore.Api.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Customer?> TryAddAsync(Customer customer, CancellationToken cancellationToken);
    Task<bool> TryUpdateAsync(int id, Customer customer, CancellationToken cancellationToken);
    Task<bool> TryDeleteAsync(int id, CancellationToken cancellationToken);

}