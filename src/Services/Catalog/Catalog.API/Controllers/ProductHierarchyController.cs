using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Requests;
using Catalog.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductHierarchyController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductHierarchyController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductHierarchyRequest request)
    {
        var query = new GetProductHierarchiesQuery(request.LevelId, request.ParentId, request.Status);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _mediator.Send(new GetProductHierarchyByIdQuery(id));
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("by-code/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var dto = await _mediator.Send(new GetProductHierarchyByCodeQuery(code));
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductHierarchyCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateProductHierarchyCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteProductHierarchyCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}

