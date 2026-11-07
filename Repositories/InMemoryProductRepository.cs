// using KodaStore.Api.Data;
// using KodaStore.Api.Models;

// namespace KodaStore.Api.Repositories;

// public sealed class InMemoryProductRepository(InMemoryProductCatalogue catalogue) : IProductRepository
// {
//     private readonly InMemoryProductCatalogue _catalogue = catalogue;

//     public IReadOnlyList<Product> GetAll() => _catalogue.GetAll();
//     public Product? GetByCode(string productCode) => _catalogue.GetByCode(productCode);

//     public bool TryAdd(Product product) => _catalogue.TryCreateProduct(product);
//     public bool TryUpdate(Product product) => _catalogue.TryUpdateProduct(product);

//     public bool TryDelete(string productCode) => _catalogue.TryDeleteProduct(productCode);
// }