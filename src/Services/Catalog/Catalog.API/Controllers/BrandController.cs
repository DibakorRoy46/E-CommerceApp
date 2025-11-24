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
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;
    public BrandController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BrandRequest request)
    {
        var query= new GetBrandsQuery(request.Status);
        var dtos = await _mediator.Send( query );
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var query = new GetBrandByIdQuery(id);
        var dto = await _mediator.Send(query);
        return Ok(dto);
    }

    [HttpGet("by-code/{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code)
    {
        var query = new GetBrandByCodeQuery(code);
        var dto = await _mediator.Send(query);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBrandCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteBrandCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
