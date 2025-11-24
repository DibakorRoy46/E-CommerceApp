using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Requests;
using Catalog.Application.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductRequest request)
        {
            var query= new GetProductsQuery(request.Status);
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var query = new GetProductByCodeQuery(code);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetProductBySpec([FromQuery] ProductSpecParams spec)
        {
            var query = new GetProductsSearchQuery(spec);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
