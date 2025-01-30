using AutoMapper;
using EventsCEC.Application.Interfaces;
using EventsCEC.Application.ViewModels;
using EventsCEC.Domain.Entities;
using EventsCEC.Domain.Repositories;

namespace EventsCEC.Application.Services;

public class EventoService : IEventoService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<Evento> _eventoRepository;

    public EventoService(IMapper mapper, IRepositoryBase<Evento> eventoRepository)
    {
        _mapper = mapper;
        _eventoRepository = eventoRepository;
    }

    public async Task<EventoViewModel> Add(EventoViewModel eventoViewModel)
    {
        var evento = _mapper.Map<Evento>(eventoViewModel);
        var categoriaResponse = await _eventoRepository.AddAsync(evento);
        _eventoRepository.SaveChanges();
        return _mapper.Map<EventoViewModel>(categoriaResponse);
    }

    public async Task Delete(Guid id)
    {
        var evento = await _eventoRepository.GetByIdAsync(id);
        if (evento is null)
            throw new ArgumentNullException(nameof(evento));
        _eventoRepository.DeleteAsync(evento);
        _eventoRepository.SaveChanges();
    }

    public async Task<IEnumerable<EventoViewModel>> Get()
    {
        var list = await _eventoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventoViewModel>>(list.OrderBy(x => x.Titulo));
    }

    public async Task<EventoViewModel> GetById(Guid id)
    {
        var evento = await _eventoRepository.GetByIdAsync(id);
        return _mapper.Map<EventoViewModel>(evento);
    }

    public async Task<EventoViewModel> Update(Guid id, EventoViewModel eventoViewModel)
    {
        var evento = await _eventoRepository.GetByIdAsync(id);
        if (evento is null)
            throw new ArgumentNullException(nameof(evento));

        evento.Titulo = eventoViewModel.Titulo;
        evento.Descricao = eventoViewModel.Descricao;
        evento.ValorInteira = eventoViewModel.ValorInteira;
        evento.ValorMeia = eventoViewModel.ValorMeia;
        evento.Responsaveis = eventoViewModel.Responsaveis;

        var eventoResponse = await _eventoRepository.UpdateAsync(evento);
        _eventoRepository.SaveChanges();
        return _mapper.Map<EventoViewModel>(eventoResponse);
    }
}
