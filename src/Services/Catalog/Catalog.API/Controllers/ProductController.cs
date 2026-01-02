using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Requests;
using Catalog.Application.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] ProductRequest request)
        {
            var query= new GetProductsQuery(request.Status);
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpGet("by-code/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var query = new GetProductByCodeQuery(code);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductBySpec([FromQuery] ProductSpecParams spec)
        {
            var query = new GetProductsSearchQuery(spec);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> BulkInsert([FromBody] List<CreateProductCommand> command)
        {
            foreach (var cmd in command)
            {
                await _mediator.Send(cmd);
            }
            return Created();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
