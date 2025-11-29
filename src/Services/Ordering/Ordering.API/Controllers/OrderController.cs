using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Specs;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] OrderSpec orderSpec)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(orderSpec));
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById([FromQuery] int orderId)
        {
            string userId = "test-user"; // In a real application, retrieve the user ID from the authenticated user context
            var result = await _mediator.Send(new GetOrderByIdQuery(orderId, userId));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("by-username/{userName}")]
        public async Task<IActionResult> GetOrderByName([FromQuery] string userName)
        {
            var result = await _mediator.Send(new GetOrderByUserNameQuery(userName));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { orderId }, null);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int orderId, [FromBody] UpdateOrderCommand command)
        {
            if (orderId != command.Order.OrderId)
                return BadRequest("Order ID mismatch");
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int orderId, [FromBody] DeleteOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
