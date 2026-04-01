using Microsoft.AspNetCore.Mvc;
using SubscriberSystem.Models;
using SubscriberSystem.Services;

namespace SubscriberSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscribersController : ControllerBase
{
    private readonly ISubscriberService _service;

    public SubscribersController(ISubscriberService service)
    {
        _service = service;
    }

    // GET /api/subscribers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscribers = await _service.GetAllAsync();
        return Ok(subscribers);
    }

    // GET /api/subscribers/export — returns all subscribers as XML
    [HttpGet("export")]
    public async Task<IActionResult> Export()
    {
        var xml = await _service.ExportToXmlAsync();
        return Content(xml, "application/xml");
    }

    // POST /api/subscribers/import — imports subscribers from XML body, skips duplicates
    [HttpPost("import")]
    public async Task<IActionResult> Import()
    {
        using var reader = new StreamReader(Request.Body);
        var xml = await reader.ReadToEndAsync();

        if (string.IsNullOrWhiteSpace(xml))
            return BadRequest("Request body is empty.");

        try
        {
            var inserted = await _service.ImportFromXmlAsync(xml);
            return Ok(new { inserted });
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Invalid XML format.");
        }
    }

    // GET /api/subscribers/{subscriptionNumber}
    [HttpGet("{subscriptionNumber}")]
    public async Task<IActionResult> GetBySubscriptionNumber(string subscriptionNumber)
    {
        var dto = await _service.GetBySubscriptionNumberAsync(subscriptionNumber);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    // POST /api/subscribers
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Subscriber subscriber)
    {
        var created = await _service.CreateAsync(subscriber);
        return CreatedAtAction(nameof(GetBySubscriptionNumber),
            new { subscriptionNumber = created.SubscriptionNumber }, created);
    }

    // DELETE /api/subscribers/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
