using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Commands;
using Notification.Application.Queries;
using Notification.Application.Requests;

namespace Notification.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
    {
        var command = new CreateNotificationCommand(request.TemplateId,request.UserId,request.Email,
                             request.PhoneNumber,request.PushToken,request.Channel,request.Payload);

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetNotificationByIdQuery(id));
        if (result == null) return NotFound();


        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(string userId)
    {
        var results = await _mediator.Send(new GetUserNotificationsQuery(userId));
        return Ok(results);
    }

    [HttpPost("{id}/mark-delivered")]
    public async Task<IActionResult> MarkDelivered(string id)
    {
        await _mediator.Send(new MarkDeliveredCommand(id));
        return NoContent();
    }
}
