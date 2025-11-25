using Basket.Application.Commands;
using Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetBasket([FromRoute] string userName)
        {
            var query= new GetBasketByUserNameQuery(userName);
            var result=await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
            var result= await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket([FromBody] DeleteBasketByUserNameCommand command)
        {
            var result= await _mediator.Send(command);
            return Ok(result);
        }
    }
}
