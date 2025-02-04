using EventsCEC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsCEC.App.Controllers
{
    [Authorize]
    public class ConfirmationController : Controller
    {
        private readonly IConvidadoService _convidadoService;

        public ConfirmationController(IConvidadoService convidadoService)
        {
            _convidadoService = convidadoService;
        }

        [Authorize]
        public async Task<IActionResult> Guest(Guid id, Guid eventId)
        {
            var convidados = await _convidadoService.GetByEventoId(eventId);

            if (convidados == null)
            {
                return View("Error");
            }

            var convidado = convidados.FirstOrDefault(x => x.Token == id);
            if (convidado == null)
            {
                return View("Error");
            }

            return View(convidado);
        }
    }
}
