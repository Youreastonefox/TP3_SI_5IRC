using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;

namespace TP3.Models.EntityFramework;

public partial class NotationDbContext : DbContext
{

    public NotationDbContext()
    {
    }

    public NotationDbContext(DbContextOptions<NotationDbContext> options)
    : base(options)
    {
    }

    public virtual DbSet<Notation> Notations { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<Serie> Series { get; set; }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=postgres;password=L1jdr2tv;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Notation>(entity =>
        {
            entity.HasKey(e => new { e.UtilisateurId, e.SerieId }).HasName("pk_not");

            entity.HasOne(d => d.UtilisateurNotant)
                .WithMany(p => p.NotesUtilisateur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_not_utl");

            entity.HasOne(d => d.SerieNotee)
                .WithMany(p => p.NotesSerie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_not_ser");

            entity.HasCheckConstraint("ck_not_note", "not_note between 0 and 5");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.UtilisateurId).HasName("pk_utl");

            entity.Property(b => b.Pays).HasDefaultValue("France");

            entity.Property(b => b.DateCreation).HasDefaultValueSql("now()");

            entity.HasIndex(e => e.Mail).IsUnique().HasName("uq_utl_mail");
        });

        modelBuilder.Entity<Serie>(entity =>
        {
            entity.HasKey(e => e.SerieId).HasName("pk_ser");

            entity.HasIndex(e => e.Titre).IsUnique(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
