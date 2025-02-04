using EventsCEC.Application.Filters;
using EventsCEC.Application.Interfaces;
using EventsCEC.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventsCEC.App.Controllers;

public class ConvidadoController : Controller
{
    private readonly IEventoService _eventoService;
    private readonly IConvidadoService _convidadoService;

    public ConvidadoController(
        IEventoService eventoService, 
        IConvidadoService convidadoService)
    {
        _eventoService = eventoService;
        _convidadoService = convidadoService;
    }

    public async Task<IActionResult> Index(int? pageNumber, int? pageSize, Guid? eventoId = null)
    {
        var convidados = await _convidadoService.Get(new ConvidadoFilter(eventoId, pageNumber, pageSize));
        await MakeViewBag(eventoId);
        return View(convidados);
    }
    [HttpGet()]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id.HasValue)
        {
            var convidado = await _convidadoService.GetById(id.Value);
            await MakeViewBag(convidado.EventoId);
            return View(convidado);
        }
        await MakeViewBag();
        return View(new ConvidadoViewModel());
    }
    [HttpPost]
    public async Task<IActionResult> Edit(ConvidadoViewModel convidadoViewModel)
    {
        if (!ModelState.IsValid)
        {
            await MakeViewBag(convidadoViewModel.EventoId);
            return View(convidadoViewModel);
        }

        if (convidadoViewModel.Id.HasValue)
            await _convidadoService.Update(convidadoViewModel.Id.Value, convidadoViewModel);
        else
            await _convidadoService.Add(convidadoViewModel);

        return RedirectToAction("Index");
    }
    [HttpGet()]
    public async Task<IActionResult> Details(Guid id)
    {
        var convidado = await _convidadoService.GetById(id);
        return View(convidado);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _convidadoService.Delete(id);
        return RedirectToAction("Index");
    }
    [HttpGet()]
    public IActionResult GetByCategoriaId(Guid id)
    {
        var lista = _convidadoService.GetByEventoId(id);
        return new JsonResult(new { data = lista.Result });
    }
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(Guid eventoId, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("file", "Nenhum arquivo foi selecionado.");
            return View("Index");
        }

        // Validação da extensão do arquivo (Excel)
        var allowedExtensions = new[] { ".xls", ".xlsx" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
        {
            ModelState.AddModelError("file", "Por favor, selecione um arquivo Excel (.xls ou .xlsx).");
            return View("Index");
        }        

        // Retorna uma mensagem de sucesso
        ViewBag.Message = "Arquivo Excel enviado com sucesso!";
        return View("Index");
    }
    
    public async Task<IActionResult> Download(string filename, Guid token)
    {
        if (string.IsNullOrEmpty(filename))
        {
            ModelState.AddModelError("file", "Houve um erro no download.");
            return View("Index");
        }

        var file = await _convidadoService.GetQrCode(token);

        return File(file, "image/png", $"{filename}.png");
    }
    private async Task MakeViewBag(Guid? id = null)
    {
        ViewBag.EventoId = new SelectList(await _eventoService.Get(), "Id", "Titulo", id ?? Guid.Empty);
    }
}
