using EventsCEC.Application.Filters;
using EventsCEC.Application.Response;
using EventsCEC.Application.ViewModels;

namespace EventsCEC.Application.Interfaces;

public interface IConvidadoService
{
    Task<IEnumerable<ConvidadoViewModel>> Get();
    Task<PagedResponse<ConvidadoViewModel>> Get(ConvidadoFilter convidadoFilter);
    Task<IEnumerable<ConvidadoViewModel>> GetByEventoId(Guid id);
    Task<ConvidadoViewModel> GetById(Guid id);
    Task<ConvidadoViewModel> Add(ConvidadoViewModel convidadoViewModel);
    Task<ConvidadoViewModel> Update(Guid id, ConvidadoViewModel convidadoViewModel);
    Task Delete(Guid id);
    Task<byte[]> GetQrCode(Guid token);
}
