using FreakyFashion.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashion.WebApi.Controllers;

[ApiController]
[Route("api/catalog")]
public sealed class CatalogController(IProductCatalog catalog) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await catalog.GetByIdAsync(id, cancellationToken);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> Search([FromQuery] string? q, CancellationToken cancellationToken) =>
        Ok(await catalog.SearchAsync(q, cancellationToken));
}
