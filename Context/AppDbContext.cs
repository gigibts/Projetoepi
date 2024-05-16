using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projetoepi.Models;

namespace projetoepi.Context;

public partial class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CadastroEpi> CadastroEpis { get; set; }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<EntregaEpi> EntregaEpis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ProjetoEpi;UserId=postgres;Password=senai901;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CadastroEpi>(entity =>
        {
            entity.HasKey(e => e.CodEpi).HasName("cadastro_epi_pkey");

            entity.ToTable("cadastro_epi");

            entity.HasIndex(e => e.CodEpi, "idx_cod_epi");

            entity.Property(e => e.CodEpi).HasColumnName("cod_epi");
            entity.Property(e => e.DescricaoUso)
                .HasMaxLength(50)
                .HasColumnName("descricao_uso");
            entity.Property(e => e.NomeEpi).HasColumnName("nome_epi");
        });

        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.CodColaborador).HasName("colaborador_pkey");

            entity.ToTable("colaborador");

            entity.HasIndex(e => e.Cpf, "colaborador_cpf_key").IsUnique();

            entity.HasIndex(e => e.CodColaborador, "idx_cod_colaborador");

            entity.HasIndex(e => e.Cpf, "idx_cod_cpf");

            entity.HasIndex(e => e.Ctps, "idx_cod_ctps");

            entity.HasIndex(e => e.Ctps, "uk_ctps").IsUnique();

            entity.Property(e => e.CodColaborador).HasColumnName("cod_colaborador");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Ctps).HasColumnName("ctps");
            entity.Property(e => e.DataAdmissao).HasColumnName("data_admissao");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasColumnType("character varying")
                .HasColumnName("nome");
            entity.Property(e => e.Telefone).HasColumnName("telefone");
        });

        modelBuilder.Entity<EntregaEpi>(entity =>
        {
            entity.HasKey(e => e.CodEntrega).HasName("entrega_epi_pkey");

            entity.ToTable("entrega_epi");

            entity.HasIndex(e => e.CodEntrega, "idx_cod_entrega");

            entity.HasIndex(e => new { e.CodEpi, e.CodColaborador }, "idx_cod_epi_colaborador");

            entity.Property(e => e.CodEntrega).HasColumnName("cod_entrega");
            entity.Property(e => e.CodColaborador).HasColumnName("cod_colaborador");
            entity.Property(e => e.CodEpi).HasColumnName("cod_epi");
            entity.Property(e => e.DataEntrega).HasColumnName("data_entrega");
            entity.Property(e => e.DataValidade).HasColumnName("data_validade");

            entity.HasOne(d => d.CodColaboradorNavigation).WithMany(p => p.EntregaEpis)
                .HasForeignKey(d => d.CodColaborador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_epi_cod_colaborador_fkey");

            entity.HasOne(d => d.CodEpiNavigation).WithMany(p => p.EntregaEpis)
                .HasForeignKey(d => d.CodEpi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_epi_cod_epi_fkey");
        });
        // modelBuilder.Ignore<CadastroEpi>();
        // modelBuilder.Ignore<Colaborador>();
        // modelBuilder.Ignore<EntregaEpi>();


        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
