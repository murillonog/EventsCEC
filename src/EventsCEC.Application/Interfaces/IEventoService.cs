using EventsCEC.Application.ViewModels;

namespace EventsCEC.Application.Interfaces;

public interface IEventoService
{
    Task<IEnumerable<EventoViewModel>> Get();
    Task<EventoViewModel> GetById(Guid id);
    Task<EventoViewModel> Add(EventoViewModel eventoViewModel);
    Task<EventoViewModel> Update(Guid id, EventoViewModel eventoViewModel);
    Task Delete(Guid id);
}
