using AutoMapper;
using EventsCEC.Application.Filters;
using EventsCEC.Application.Interfaces;
using EventsCEC.Application.Response;
using EventsCEC.Application.ViewModels;
using EventsCEC.Domain.Entities;
using EventsCEC.Domain.Repositories;

namespace EventsCEC.Application.Services;

public class ConvidadoService : IConvidadoService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<Convidado> _convidadoRepository;

    public ConvidadoService(IMapper mapper, IRepositoryBase<Convidado> convidadoRepository)
    {
        _mapper = mapper;
        _convidadoRepository = convidadoRepository;
    }

    public async Task<ConvidadoViewModel> Add(ConvidadoViewModel convidadoViewModel)
    {
        var convidado = _mapper.Map<Convidado>(convidadoViewModel);
        var convidadoResponse = await _convidadoRepository.AddAsync(convidado);
        _convidadoRepository.SaveChanges();
        return _mapper.Map<ConvidadoViewModel>(convidadoResponse);
    }

    public async Task Delete(Guid id)
    {
        var convidado = await _convidadoRepository.GetByIdAsync(id);
        if (convidado is null)
            throw new ArgumentNullException(nameof(convidado));
        _convidadoRepository.DeleteAsync(convidado);
        _convidadoRepository.SaveChanges();
    }

    public async Task<IEnumerable<ConvidadoViewModel>> Get()
    {
        var list = await _convidadoRepository.GetAllAsync(x => x.EventoId);
        return _mapper.Map<IEnumerable<ConvidadoViewModel>>(list.OrderBy(x => x.Nome));
    }

    public async Task<PagedResponse<ConvidadoViewModel>> Get(ConvidadoFilter convidadoFilter)
    {
        var list = await _convidadoRepository.GetAllAsync(x => x.Evento);

        if (convidadoFilter.EventoId.HasValue)
            list = list.Where(x => x.EventoId == convidadoFilter.EventoId);

        var result = _mapper.Map<IEnumerable<ConvidadoViewModel>>(list.OrderBy(x => x.Nome)).AsQueryable();

        return PagedResponse<ConvidadoViewModel>.Create(result, convidadoFilter.PageNumber, convidadoFilter.PageSize);
    }

    public async Task<IEnumerable<ConvidadoViewModel>> GetByEventoId(Guid id)
    {
        var list = await _convidadoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ConvidadoViewModel>>(list.Where(x => x.EventoId == id).OrderBy(x => x.Nome));
    }

    public async Task<ConvidadoViewModel> GetById(Guid id)
    {
        var convidado = await _convidadoRepository.GetByIdAsync(id, x => x.Evento);
        return _mapper.Map<ConvidadoViewModel>(convidado);
    }

    public async Task<ConvidadoViewModel> Update(Guid id, ConvidadoViewModel convidadoViewModel)
    {
        var convidado = await _convidadoRepository.GetByIdAsync(id);
        if (convidado is null)
            throw new ArgumentNullException(nameof(convidado));

        convidado.Nome = convidadoViewModel.Nome;
        convidado.Email = convidadoViewModel.Email;
        convidado.Telefone = convidadoViewModel.Telefone;
        convidado.Pago = convidadoViewModel.Pago;
        convidado.Meia = convidadoViewModel.Meia;

        var convidadoResponse = await _convidadoRepository.UpdateAsync(convidado);
        _convidadoRepository.SaveChanges();
        return _mapper.Map<ConvidadoViewModel>(convidadoResponse);
    }
}
