using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Queries;

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

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserNotifications(string userId,CancellationToken cancellationToken)
    {
        var query = new GetUserNotificationsQuery(userId);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}
