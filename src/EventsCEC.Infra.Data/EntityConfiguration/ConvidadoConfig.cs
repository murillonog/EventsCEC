using EventsCEC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCEC.Infra.Data.EntityConfiguration;

public class ConvidadoConfig : IEntityTypeConfiguration<Convidado>
{
    public void Configure(EntityTypeBuilder<Convidado> builder)
    {
        EntityBaseConfiguration<Convidado>.Configure(builder);

        builder.Property(e => e.Nome);
        builder.Property(e => e.Email);
        builder.Property(e => e.Telefone);
        builder.Property(e => e.Idade);
        builder.Property(e => e.Meia);
        builder.Property(e => e.Pago);

        builder.HasOne<Evento>(me => me.Evento)
                .WithMany(parent => parent.Convidados)
                .HasForeignKey(me => me.EventoId)
                .HasConstraintName("FK_Convidado_To_Evento_EventoId")
                .OnDelete(DeleteBehavior.ClientNoAction);
    }
}
