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
        try
        {
            var query = new GetProductHierarchiesQuery(levelId, parentId, status);
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            throw;
        }
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var query = new GetProductHierarchyByIdQuery(id);
            var dto = await _mediator.Send(query);
            if (dto is null) return NotFound();
            return Ok(dto);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpGet("GetByCode/{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code)
    {
        try
        {
            var query = new GetProductHierarchyByCodeQuery(code);
            var dto = await _mediator.Send(query);
            if (dto is null) return NotFound();
            return Ok(dto);
        }
        catch (Exception ex)
        {
            throw;
        }   
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductHierarchyCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductHierarchyCommand command)
    {
        try
        {
            if (id != command.Id) return BadRequest("Id mismatch");
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductHierarchy(int id, DeleteProductHierarchyCommand command)
    {
        try
        {
            if (id != command.Id) return BadRequest("Id mismatch");
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex) 
        {
            throw;
        }
    }
}
