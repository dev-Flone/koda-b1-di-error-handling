using KodaStore.Api.DTOs;
using KodaStore.Api.Models;
using KodaStore.Api.Repositories;

namespace KodaStore.Api.Services;

public sealed class CustomerService(ICustomerRepository repository) : ICustomerService
{
    private readonly ICustomerRepository _repository = repository;

    public async Task<CustomerResponse?> CreateAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name, request.Email, request.Phone, request.Address);

        var created = await _repository.TryAddAsync(customer, cancellationToken);
        return created is null ? null : CustomerResponse.FromModel(created);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return Task.FromResult(false);
        }
        return _repository.TryDeleteAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<CustomerResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAllAsync(cancellationToken);
        return [.. customers.Select(CustomerResponse.FromModel)];
    }

    public async Task<CustomerDetailResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return null;
        }

        var customer = await _repository.GetByIdAsync(id, cancellationToken);
        if (customer is null)
        {
            return null;
        }

        return new CustomerDetailResponse(
            customer.Id,
            customer.Name,
            customer.Email,
            customer.Phone,
            customer.Address,
            [.. customer.Products.Select(product => new CustomerProductResponse(product.ProductCode, product.Name))]);
    }

    public async Task<CustomerResponse?> UpdateAsync(int id, UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name, request.Email, request.Phone, request.Address);

        if (id <= 0)
        {
            return null;
        }

        var updated = await _repository.TryUpdateAsync(id, customer, cancellationToken);
        return updated
            ? CustomerResponse.FromModel(new Customer(id, customer.Name, customer.Email, customer.Phone, customer.Address))
            : null;
    }
}