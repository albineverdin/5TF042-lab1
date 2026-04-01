using Microsoft.AspNetCore.Mvc;
using AdSystem.Models;
using AdSystem.Services;

namespace AdSystem.Controllers;

// Dev-only controller for inspecting and seeding data during development
public class DevController : Controller
{
    private readonly IAdService _adService;

    public DevController(IAdService adService)
    {
        _adService = adService;
    }

    // GET /Dev — show all data
    public async Task<IActionResult> Index()
    {
        var subscribers = await _adService.GetAllSubscribersAsync();
        var ads = await _adService.GetAllAdsAsync();
        ViewBag.Subscribers = subscribers;
        ViewBag.Ads = ads;
        return View();
    }

    // POST /Dev/CreateSubscriber
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSubscriber(SubscriberRecord model)
    {
        await _adService.CreateSubscriberAsync(model);
        return RedirectToAction(nameof(Index));
    }

    // POST /Dev/DeleteSubscriber
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSubscriber(int id)
    {
        await _adService.DeleteSubscriberAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // POST /Dev/DeleteAd
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAd(int id)
    {
        await _adService.DeleteAdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}