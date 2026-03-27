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
