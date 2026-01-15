using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using ContactManager.Models.Entity;
using ContactManager.Service.Interfaces;

namespace ContactManager.Controllers;

public class ContactController(IContactService service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allListContact = await service.GetAllAsync();
        return View(allListContact);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Contact contact)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await service.AddAsync(contact);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();
        
        var entity = await service.GetByIdAsync(id);
        if (entity == null)
            return NotFound();
        
        return View(entity);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, Contact contact)
    {
        if (!ModelState.IsValid || id == Guid.Empty)
            return BadRequest();
        
        await service.UpdateAsync(id, contact);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();
        
        await service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}