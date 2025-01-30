namespace EventsCEC.Application.Filters;

public class ConvidadoFilter : PaginationFilter
{
    public Guid? EventoId { get; set; }
    public ConvidadoFilter(Guid? EventoId) : base()
    {
        this.EventoId = EventoId;
    }

    public ConvidadoFilter(Guid? EventoId, int? pageNumber, int? pageSize) : base(pageNumber, pageSize)
    {
        this.EventoId = EventoId;
    }
}
