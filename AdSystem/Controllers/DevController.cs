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

    // GET /Dev/DownloadXml — fetch XML from API and return as file download
    public async Task<IActionResult> DownloadXml()
    {
        var xml = await _adService.ExportSubscribersXmlAsync();
        if (xml == null) return StatusCode(502, "Could not reach Subscriber API.");
        var bytes = System.Text.Encoding.UTF8.GetBytes(xml);
        return File(bytes, "application/xml", "subscribers.xml");
    }

    // POST /Dev/ImportXml — upload an XML file and import subscribers
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportXml(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["ImportError"] = "No file selected.";
            return RedirectToAction(nameof(Index));
        }

        using var reader = new System.IO.StreamReader(file.OpenReadStream());
        var xml = await reader.ReadToEndAsync();

        var inserted = await _adService.ImportSubscribersXmlAsync(xml);
        if (inserted == null)
        {
            TempData["ImportError"] = "Import failed. Check that the XML format is correct.";
        }
        else
        {
            TempData["ImportSuccess"] = $"{inserted} subscriber(s) imported.";
        }

        return RedirectToAction(nameof(Index));
    }
}