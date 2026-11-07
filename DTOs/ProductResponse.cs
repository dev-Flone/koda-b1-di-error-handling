using KodaStore.Api.Models;

namespace KodaStore.Api.DTOs;

public sealed record ProductResponse(int CustomerId, string ProductCode, string Name, long Price, int Stock, string Category)

{
    public static ProductResponse FromModel(Product product) => new(product.CustomerId, product.ProductCode, product.Name, product.Price, product.Stock, product.Category);
}

public sealed record CreateProductRequest(string ProductCode, string Name, long Price, int Stock, string Category);

public record UpdateProductRequest(int CustomerId, string Name, long Price, int Stock, string Category);