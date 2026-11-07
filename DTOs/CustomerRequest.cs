using System.ComponentModel.DataAnnotations;

namespace KodaStore.Api.DTOs;

public record CustomerRequest(
    [Required, StringLength(50)]
    string Name,

    [Required, StringLength(100)]
    string Email,

    [Required, StringLength(15)]
    string Phone,

    [Required, StringLength(255)]
    string Address
    );