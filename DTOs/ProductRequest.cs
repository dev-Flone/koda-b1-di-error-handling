using System.ComponentModel.DataAnnotations;

namespace KodaStore.Api.DTOs;

public record ProductRequest(
    [Required, Range(1, int.MaxValue)]
    int CustomerId,

    [Required, StringLength(30)]
    string ProductCode,

    [Required, StringLength(120)]
    string Name,

    [Required, Range(typeof(long), "0", "9999999999")]
    long Price,

    [Required, Range(0, int.MaxValue)]
    int Stock,

    [Required, StringLength(120)]
    string Category
    );