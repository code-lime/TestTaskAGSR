using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Immutable;
using TaskAGSR.Domain.Entities;
using TaskAGSR.Infrastructure.DataBase.Extensions;

namespace TaskAGSR.Infrastructure.DataBase;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; } = null!;

    public DbContext DbContext => this;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Patient>(entity =>
            {
                entity.ToTable("patients");

                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Use)
                    .HasConversion<string>();
                entity.Property(e => e.Family);
                entity.Property(e => e.Given)
                    .HasConversion(
                        new JsonConverter<ImmutableArray<string>>(),
                        new ValueComparer<ImmutableArray<string>>(true));
                entity.Property(e => e.Gender)
                    .HasConversion<string>();
                entity.Property(e => e.BirthDate);
                entity.Property(e => e.Active);
            });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
