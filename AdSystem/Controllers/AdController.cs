using Microsoft.AspNetCore.Mvc;
using AdSystem.Models.ViewModels;
using AdSystem.Services;

namespace AdSystem.Controllers;

public class AdController : Controller
{
    private readonly IAdService _adService;

    public AdController(IAdService adService)
    {
        _adService = adService;
    }

    // GET /Ad/Create — show the ad creation form
    public IActionResult Create()
    {
        return View();
    }

    // GET /Ad/LookupSubscriber?subscriptionNumber=xxx — called via AJAX to pre-fill form
    [HttpGet]
    public async Task<IActionResult> LookupSubscriber(string subscriptionNumber)
    {
        var subscriber = await _adService.LookupSubscriberAsync(subscriptionNumber);
        if (subscriber == null)
            return NotFound();

        return Json(subscriber);
    }

    // POST /Ad/CreateSubscriberAd
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSubscriberAd(SubscriberAdViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Create", model);

        try
        {
            await _adService.CreateSubscriberAdAsync(
                model.SubscriptionNumber,
                model.Name,
                model.Phone,
                model.Address,
                model.PostalCode,
                model.City,
                model.Title,
                model.Content,
                model.ItemPrice);

            return RedirectToAction(nameof(List));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Create", model);
        }
    }

    // POST /Ad/CreateCompanyAd
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCompanyAd(CompanyAdViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Create", model);

        await _adService.CreateCompanyAdAsync(
            model.OrganisationNumber,
            model.Name,
            model.Phone,
            model.Address,
            model.PostalCode,
            model.City,
            model.InvoiceAddress,
            model.InvoicePostalCode,
            model.InvoiceCity,
            model.Title,
            model.Content,
            model.ItemPrice);

        return RedirectToAction(nameof(List));
    }

    // GET /Ad/List — show all ads
    public async Task<IActionResult> List()
    {
        var ads = await _adService.GetAllAdsAsync();
        return View(ads);
    }
}
