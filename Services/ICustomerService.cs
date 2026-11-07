using KodaStore.Api.DTOs;

namespace KodaStore.Api.Services;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<CustomerDetailResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<CustomerResponse?> CreateAsync(CustomerRequest request, CancellationToken cancellationToken);
    Task<CustomerResponse?> UpdateAsync(int id, UpdateCustomerRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

}