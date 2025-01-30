namespace EventsCEC.Domain.Entities;

public class Convidado : EntityBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public int Idade { get; set; }
    public bool Meia { get; set; }
    public bool Pago { get; set; }
    public Guid Token { get; set; }
    public Guid EventoId { get; set; }
    public Evento Evento { get; set; }
}
