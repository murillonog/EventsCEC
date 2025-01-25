namespace EventsCEC.Domain.Entities;

public class EntityBase
{
    public Guid Id { get; protected set; }
    public string CreatedBy { get; set; }
    public DateTime? Created { get; set; }
    public string? ModifiedBy { get; protected set; }
    public DateTime? Modified { get; set; }
}
