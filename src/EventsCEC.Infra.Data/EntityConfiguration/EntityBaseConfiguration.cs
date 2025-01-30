using EventsCEC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCEC.Infra.Data.EntityConfiguration;

public static class EntityBaseConfiguration<T> where T : EntityBase
{
    public static void Configure(EntityTypeBuilder<T> builder)
    {
        //Attributes
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Created)
            .HasColumnType("timestamp");

        builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

        builder.Property(e => e.Modified)
                .HasColumnType("timestamp");

        builder.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
    }
}
