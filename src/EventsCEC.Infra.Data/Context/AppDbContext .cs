using EventsCEC.Domain.Entities;
using EventsCEC.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsCEC.Infra.Data.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region [Configurations]
        #endregion

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SaveRegister();

        return await base.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        SaveRegister();

        return base.SaveChanges();
    }

    private void SaveRegister()
    {
        var entries = ChangeTracker
                        .Entries()
                        .Where(e => e.Entity is EntityBase && (
                                e.State == EntityState.Added
                                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((EntityBase)entityEntry.Entity).Created = DateTime.Now;
                if (string.IsNullOrEmpty(((EntityBase)entityEntry.Entity).CreatedBy))
                    ((EntityBase)entityEntry.Entity).CreatedBy = "anonymous";
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                ((EntityBase)entityEntry.Entity).Modified = DateTime.Now;
            }
        }
    }
}
