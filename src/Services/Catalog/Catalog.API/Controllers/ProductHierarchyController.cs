using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Requests;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductHierarchyController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductHierarchyController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromQuery] ProductHierarchyRequest request)
    {
        var query = new GetProductHierarchiesQuery(request.LevelId, request.ParentId, request.Status);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _mediator.Send(new GetProductHierarchyByIdQuery(id));
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("by-code/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByCode(string code)
    {
        var dto = await _mediator.Send(new GetProductHierarchyByCodeQuery(code));
        return dto is null ? NotFound() : Ok(dto);
    }

    [AllowAnonymous]
    [HttpGet("product-categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ProductCategoryResponse>>> GetProductCategories()
    {
        var result = await _mediator.Send(new GetProductCategoriesQuery());
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductHierarchyCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPost("BulkInsert")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateBulk([FromBody] List<CreateProductHierarchyCommand> command)
    {
        foreach (var item in command)
        {
            await _mediator.Send(item);
        }
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateProductHierarchyCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteProductHierarchyCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}

