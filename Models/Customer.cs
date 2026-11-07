namespace KodaStore.Api.Models;

public sealed class Customer
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string Phone { get; }
    public string Address { get; }
    public IReadOnlyList<CustomerProduct> Products { get; }

    public Customer(string name, string email, string phone, string address)
        : this(0, name, email, phone, address, [])
    {
    }

    public Customer(int id, string name, string email, string phone, string address, IReadOnlyList<CustomerProduct>? products = null)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Customer ID cannot be negative.");
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required");
        }
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Phone is required");
        }
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address is required");
        }

        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        Products = products ?? [];
    }
}