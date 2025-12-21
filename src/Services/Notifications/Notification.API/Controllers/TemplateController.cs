using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Requests;
using Notification.Application.Commands;
using Notification.Application.Queries;

namespace Notification.API.Controllers;

[ApiController]
[Route("api/templates")]
public class TemplatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTemplateCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetAll), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromRoute] TempleteRequest request)
    {
        var result = await _mediator.Send(new GetAllTempletesQuery(request.Type, request.channel, request.IsActive));

        return Ok(result);
    }

    [HttpPut("{code}/activate")]
    public async Task<IActionResult> Activate(string code)
    {
        await _mediator.Send(new ActivatedTemplateCommand(code));

        return NoContent();
    }

    [HttpPut("{code}/deactivate")]
    public async Task<IActionResult> Deactivate(string code)
    {
        await _mediator.Send(new DeActivatedTemplateCommand(code));

        return NoContent();
    }
}
