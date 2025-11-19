using Catalog.Application.Commands;
using Catalog.Application.Queries;
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
    public ProductHierarchyController(IMediator mediator) => _mediator =  mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(ProductHierarchyLevelEnum? levelId, int? parentId,StatusEnum status)
    {
        var query = new GetProductHierarchiesQuery(levelId,parentId,status);
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var query = new GetProductHierarchyByIdQuery(id);
        var dto = await _mediator.Send(query);
        if (dto is null) return NotFound();
        return Ok(dto);
    }

    [HttpGet("GetByCode/{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code)
    {
        var query = new GetProductHierarchyByCodeQuery(code);
        var dto = await _mediator.Send(query);
        if (dto is null) return NotFound();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductHierarchyCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductHierarchyCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductHierarchy(int id, DeleteProductHierarchyCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await _mediator.Send(command);
        return NoContent();
    }
}
