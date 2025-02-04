using System.ComponentModel.DataAnnotations;

namespace EventsCEC.Application.ViewModels;

public class ConvidadoViewModel
{
    public Guid? Id { get; set; }
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public int Idade { get; set; }
    public bool Meia { get; set; }
    public bool Pago { get; set; }

    public Guid? Token { get; set; }
    public Guid? EventoId { get; set; }
    public EventoViewModel? Evento { get; set; }

}
