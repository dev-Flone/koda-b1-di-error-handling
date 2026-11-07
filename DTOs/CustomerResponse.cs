using System.Collections.Generic;
using KodaStore.Api.Models;

namespace KodaStore.Api.DTOs;

public sealed record CustomerResponse(int Id, string Name, string Email, string Phone, string Address)

{
    public static CustomerResponse FromModel(Customer customer) => new(customer.Id, customer.Name, customer.Email, customer.Phone, customer.Address);
}

public sealed record CustomerDetailResponse(
    int Id,
    string Name,
    string Email,
    string Phone,
    string Address,
    IReadOnlyList<CustomerProductResponse> Products);

public sealed record CustomerProductResponse(
    string ProductCode,
    string Name);

public sealed record CreateCustomerRequest(string Name, string Email, string Phone, string Address);

public record UpdateCustomerRequest(string Name, string Email, string Phone, string Address);