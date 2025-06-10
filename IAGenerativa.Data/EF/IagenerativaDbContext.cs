using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IAGenerativa.Data.EF;

public partial class IagenerativaDbContext : DbContext
{
    public IagenerativaDbContext()
    {
    }

    public IagenerativaDbContext(DbContextOptions<IagenerativaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ambito> Ambitos { get; set; }

    public virtual DbSet<Clasificacion> Clasificacions { get; set; }

    public virtual DbSet<EstadosAnimo> EstadosAnimos { get; set; }

    public virtual DbSet<ResultadoAnalizadorDeTexto> ResultadoAnalizadorDeTextos { get; set; }

    public virtual DbSet<ResultadoAnalizadorEstadoAnimo> ResultadoAnalizadorEstadoAnimos { get; set; }

    public virtual DbSet<ResultadoAnalizadorOracione> ResultadoAnalizadorOraciones { get; set; }

    public virtual DbSet<ResultadoTransformadorDeTexto> ResultadoTransformadorDeTextos { get; set; }

    public virtual DbSet<TipoEstadoAnimo> TipoEstadoAnimos { get; set; }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ambito>(entity =>
        {
            entity.ToTable("Ambito");

            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Clasificacion>(entity =>
        {
            entity.ToTable("Clasificacion");

            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<EstadosAnimo>(entity =>
        {
            entity.ToTable("EstadosAnimo");

            entity.Property(e => e.Nombre).HasMaxLength(55);

            entity.HasOne(d => d.Tipo).WithMany(p => p.EstadosAnimos)
                .HasForeignKey(d => d.TipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadosAnimo_TipoEstadosAnimo");
        });

        modelBuilder.Entity<ResultadoAnalizadorDeTexto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ResultadoClasificacion_1");

            entity.ToTable("ResultadoAnalizadorDeTexto");

            entity.Property(e => e.FechaProcesamiento).HasColumnType("datetime");

            entity.HasOne(d => d.Ambito).WithMany(p => p.ResultadoAnalizadorDeTextos)
                .HasForeignKey(d => d.AmbitoId)
                .HasConstraintName("FK_ResultadoAnalizadorDeTexto_Ambito");

            entity.HasOne(d => d.Clasificacion).WithMany(p => p.ResultadoAnalizadorDeTextos)
                .HasForeignKey(d => d.ClasificacionId)
                .HasConstraintName("FK_ResultadoAnalizadorDeTexto_Clasificacion");

            entity.HasOne(d => d.TipoEstadoAnimo).WithMany(p => p.ResultadoAnalizadorDeTextos)
                .HasForeignKey(d => d.TipoEstadoAnimoId)
                .HasConstraintName("FK_ResultadoAnalizadorDeTexto_TipoEstadosAnimo");
        });

        modelBuilder.Entity<ResultadoAnalizadorEstadoAnimo>(entity =>
        {
            entity.ToTable("ResultadoAnalizadorEstadoAnimo");

            entity.Property(e => e.FechaProcesamiento).HasColumnType("datetime");
            entity.Property(e => e.TextoOriginal).IsUnicode(false);

            entity.HasOne(d => d.TipoEstadoAnimo).WithMany(p => p.ResultadoAnalizadorEstadoAnimos)
                .HasForeignKey(d => d.TipoEstadoAnimoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultadoAnalizadorEstadoAnimo_TipoEstadoAnimo");
        });

        modelBuilder.Entity<ResultadoAnalizadorOracione>(entity =>
        {
            entity.Property(e => e.FechaProcesamiento).HasColumnType("datetime");
            entity.Property(e => e.TextoOriginal).IsUnicode(false);

            entity.HasOne(d => d.Clasificacion).WithMany(p => p.ResultadoAnalizadorOraciones)
                .HasForeignKey(d => d.ClasificacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultadoAnalizadorOraciones_Clasificacion");
        });

        modelBuilder.Entity<ResultadoTransformadorDeTexto>(entity =>
        {
            entity.ToTable("ResultadoTransformadorDeTexto");

            entity.Property(e => e.FechaProcesamiento).HasColumnType("datetime");
            entity.Property(e => e.TextoOriginal).IsUnicode(false);
            entity.Property(e => e.TextoTransformado).IsUnicode(false);

            entity.HasOne(d => d.Ambito).WithMany(p => p.ResultadoTransformadorDeTextos)
                .HasForeignKey(d => d.AmbitoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultadoTransformadorDeTexto_Ambito");
        });

        modelBuilder.Entity<TipoEstadoAnimo>(entity =>
        {
            entity.ToTable("TipoEstadoAnimo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<PalabraClave> PalabrasClave { get; set; }
    public DbSet<PalabraClaveAmbito> PalabraClaveAmbitos { get; set; }

}
