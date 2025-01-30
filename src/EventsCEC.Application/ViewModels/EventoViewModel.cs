namespace EventsCEC.Application.ViewModels;

public class EventoViewModel
{
    public Guid? Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public decimal ValorInteira { get; set; }
    public decimal ValorMeia { get; set; }
    public string Responsaveis { get; set; }
}
