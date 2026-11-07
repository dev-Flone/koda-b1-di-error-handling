using KodaStore.Api.DTOs;
using KodaStore.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace KodaStore.Api.Controllers;

[ApiController]
[Route("api/v1/customers")]

public sealed class CustomerController(ICustomerService service) : ControllerBase
{
    private readonly ICustomerService _service = service;

    // GET ALL CUSTOMER
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var customers = await _service.GetAllAsync(cancellationToken);
        return Ok(customers);
    }

    // GET CUSTOMER DETAILS
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CustomerDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailResponse>> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var customer = await _service.GetByIdAsync(id, cancellationToken);

        return customer is null ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Customer not found.") : Ok(customer);
    }

    // POST CUSTOMER
    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CustomerResponse>> Create([FromBody] CustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var created = await _service.CreateAsync(request, cancellationToken);

            if (created is null)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, title: "A customer with this email or phone already exists.");
            }
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException e)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid Customer data", detail: e.Message);
        }
    }

    // DELETE CUSTOMER
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await _service.DeleteAsync(id, cancellationToken);
            return deleted ? NoContent() : Problem(statusCode: StatusCodes.Status404NotFound, title: "Customer not found.", detail: $"There is no customer with ID {id}.");
        }
        catch (InvalidOperationException)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Customer has products.",
                detail: "Delete or reassign this customer's products before deleting the customer.");
        }
    }

    // UPDATE CUSTOMER
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerResponse>> Update([FromRoute] int id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, request, cancellationToken);
            return updated is null ? Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Customer not found.",
                detail: $"There is no customer with ID {id}."
            ) : Ok(updated);
        }
        catch (ArgumentException e)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid customer", detail: e.Message);
        }
    }
}