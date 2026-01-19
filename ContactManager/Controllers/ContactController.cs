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

            TempData["Toast.Type"] = "success";
            TempData["Toast.Message"] = $"Contact \"{entity.Name}\" created";

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
                TempData["Toast.Type"] = "warning";
                TempData["Toast.Message"] = "Contact was not updated";

                logger.LogWarning("Contact {Id} was not updated", model.Id);
                return RedirectToAction(nameof(Index));
            }

            TempData["Toast.Type"] = "success";
            TempData["Toast.Message"] = $"Contact \"{model.Name}\" updated";

            logger.LogInformation("Contact updated. Name: {Name}", model.Name);
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
                TempData["Toast.Type"] = "warning";
                TempData["Toast.Message"] = "Contact was not deleted";

                logger.LogWarning("Contact {Id} was not deleted", id);
                return RedirectToAction(nameof(Index));
            }

            TempData["Toast.Type"] = "success";
            TempData["Toast.Message"] = "Contact deleted";

            logger.LogInformation("Contact {Id} was deleted.", id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when deleted the contact.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BulkDelete(List<Guid>? ids)
    {
        if (ids == null || ids.Count == 0)
        {
            TempData["Toast.Type"] = "warning";
            TempData["Toast.Message"] = "No contacts selected";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var deletedCount = await service.DeleteRangeAsync(ids);

            if (deletedCount == 0)
            {
                TempData["Toast.Type"] = "warning";
                TempData["Toast.Message"] = "No contacts were deleted";

                logger.LogWarning("Bulk delete: no entities deleted");
                return RedirectToAction(nameof(Index));
            }

            TempData["Toast.Type"] = "success";
            TempData["Toast.Message"] = $"{deletedCount} contacts deleted";

            logger.LogInformation("Bulk delete executed. Deleted: {Count}", deletedCount);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "An error occurred when deleting the contacts");
            return RedirectToAction(nameof(Index));
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}