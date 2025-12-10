using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;

namespace Notification.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TemplateController : ControllerBase
{
    private readonly ITemplateRepository _templateService;


    public TemplateController(ITemplateRepository templateService)
    {
        _templateService = templateService;
    }


    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //{
    //    var templates = await _templateService.GetAllAsync();
    //    return Ok(templates);
    //}


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var template = await _templateService.GetByIdAsync(id);
        if (template == null) return NotFound();
        return Ok(template);
    }

    //[HttpPost]
    //public async Task<IActionResult> Create([FromBody] CreateTemplateRequest request)
    //{
    //    var id = await _templateService.CreateAsync(request.Name, request.Content, request.Locale);
    //    return CreatedAtAction(nameof(Get), new { id }, new { id });
    //}
}