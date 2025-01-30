using EventsCEC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCEC.Infra.Data.EntityConfiguration;

public class EventoConfig : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        EntityBaseConfiguration<Evento>.Configure(builder);

        builder.Property(e => e.Titulo);
        builder.Property(e => e.Descricao);
        builder.Property(e => e.ValorInteira);
        builder.Property(e => e.ValorMeia);
        builder.Property(e => e.Responsaveis);
    }
}
