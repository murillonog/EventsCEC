namespace EventsCEC.Domain.Entities;

public class Evento : EntityBase
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public decimal ValorInteira { get; set; }
    public decimal ValorMeia { get; set; }
    public string Responsaveis { get; set; }

    public ICollection<Convidado> Convidados { get; set; }
}
