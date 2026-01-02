using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DiscountController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountController(ILogger<DiscountController> logger, IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllCoupons([FromQuery] int? isActive)
    {
        var coupnQuery = new GetCouponsQuery(isActive);
        var result = await _mediator.Send(coupnQuery);
        return Ok(result);
    }

    [HttpGet("{couponId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCoupon(int couponId)
    {
        var coupnQuery = new GetCouponByIdQuery(couponId);
        var result = await _mediator.Send(coupnQuery);
        return Ok(result);
    }

    [HttpGet("by-code/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCouponByCode(string code)
    {
        var coupnQuery = new GetCouponByCodeQuery(code);
        var result = await _mediator.Send(coupnQuery);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCoupon), new { couponId = result.Id }, result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateCoupon(int id,[FromBody] UpdateCouponCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Coupon ID mismatch between URL and body.");
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteCoupon(int id)
    {
        var deleteCommand = new DeleteCouponCommand(id);
        var result = await _mediator.Send(deleteCommand);
        return Ok(result);
    }
}
