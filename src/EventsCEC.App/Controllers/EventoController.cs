using EventsCEC.Application.Interfaces;
using EventsCEC.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventsCEC.App.Controllers;

public class EventoController : Controller
{
    private readonly IEventoService _eventoService;

    public EventoController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet()]
    public async Task<IActionResult> Index()
    {
        var eventos = await _eventoService.Get();
        return View(eventos);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id.HasValue)
        {
            var evento = await _eventoService.GetById(id.Value);
            return View(evento);
        }
        return View(new EventoViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EventoViewModel eventoViewModel)
    {
        if (!ModelState.IsValid)
            return View(eventoViewModel);

        if (eventoViewModel.Id.HasValue)
            await _eventoService.Update(eventoViewModel.Id.Value, eventoViewModel);
        else
           await _eventoService.Add(eventoViewModel);

        return RedirectToAction("Index");
    }
    [HttpGet()]
    public async Task<IActionResult> Details(Guid id)
    {
        var evento = await _eventoService.GetById(id);
        return View(evento);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _eventoService.Delete(id);
        return RedirectToAction("Index");
    }
}
