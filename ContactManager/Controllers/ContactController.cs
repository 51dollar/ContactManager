using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using ContactManager.Models.ViewModels;
using ContactManager.Service.Interfaces;

namespace ContactManager.Controllers;

public class ContactController(
    IContactService service,
    ILogger<ContactController> logger
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var getAllData = new IndexViewModel
            {
                Contacts = await service.GetAllAsync(),
                Contact = new ContactViewModel()
            };

            logger.LogInformation("Retrieved {Count} contacts.", getAllData.Contacts.Count);

            return View(getAllData);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when retrieving all contacts.");
            return RedirectToAction(nameof(Error));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            
            logger.LogWarning("Validation failed: {Errors}", string.Join("; ", errors));
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var entity = await service.AddAsync(model);
            
            logger.LogInformation("Contact created. Name: {Name}", entity.Name);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when creating the contact");
            return RedirectToAction(nameof(Index));
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ContactViewModel model)
    {
        if (!ModelState.IsValid || model.Id == null)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            
            logger.LogWarning("Validation failed: {Errors}", string.Join("; ", errors));
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var updateEntity = await service.UpdateAsync(id, model);
            if (!updateEntity)
            {
                logger.LogWarning("Contact {Id} was not updated", model.Id);
                return RedirectToAction(nameof(Index));
            }
            
            logger.LogInformation("Contact updated. Name: {Name}",  model.Name);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when updating the contact.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty)
        {
            logger.LogWarning("Contact id is not found");
            return BadRequest();
        }

        try
        {
            var deleteEntity = await service.DeleteAsync(id);
            if (!deleteEntity)
            {
                logger.LogWarning("Contact {Id} was not deleted", id);
                return RedirectToAction(nameof(Index));
            }
            
            logger.LogInformation("Contact {Id} was deleted.", id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when deleted the contact.");
            return RedirectToAction(nameof(Index));
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}