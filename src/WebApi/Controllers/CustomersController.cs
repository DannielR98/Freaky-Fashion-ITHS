using FreakyFashion.Customers;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashion.WebApi.Controllers;

[ApiController]
[Route("api/customers")]
public sealed class CustomersController(ICustomerDirectory customers) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await customers.GetByIdAsync(id, cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }
}
