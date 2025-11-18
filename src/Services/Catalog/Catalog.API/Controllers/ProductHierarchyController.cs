using Catalog.Application.Commands;
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

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductHierarchyCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        //var query = new GetProductHierarchyByIdQuery(id);
        //var dto = await _mediator.Send(query);
        //if (dto is null) return NotFound();
        return Ok();
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
