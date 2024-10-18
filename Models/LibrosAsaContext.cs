using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASA.Models;

public partial class LibrosAsaContext : DbContext
{
    public LibrosAsaContext()
    {
    }

    public LibrosAsaContext(DbContextOptions<LibrosAsaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatCategoria> CatCategorias { get; set; }

    public virtual DbSet<CatEstado> CatEstados { get; set; }

    public virtual DbSet<CatLibro> CatLibros { get; set; }

    public virtual DbSet<CatSubcategoria> CatSubcategorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=LibrosASA;User Id=evaluacion;Password=evaluacion;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatCategoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.ToTable("cat_categorias");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.IdSubcategoria).HasColumnName("id_subcategoria");

            entity.HasOne(d => d.IdSubcategoriaNavigation).WithMany(p => p.CatCategoria)
                .HasForeignKey(d => d.IdSubcategoria)
                .HasConstraintName("FK_cat_categorias_cat_subcategorias");
        });

        modelBuilder.Entity<CatEstado>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("cat_estado");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<CatLibro>(entity =>
        {
            entity.HasKey(e => e.IdLibro);

            entity.ToTable("cat_libros");

            entity.Property(e => e.IdLibro).HasColumnName("id_libro");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.CatLibros)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cat_libros_cat_categorias");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.CatLibros)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cat_libros_cat_estado");
        });

        modelBuilder.Entity<CatSubcategoria>(entity =>
        {
            entity.HasKey(e => e.IdSubcategoria);

            entity.ToTable("cat_subcategorias");

            entity.Property(e => e.IdSubcategoria).HasColumnName("id_subcategoria");
            entity.Property(e => e.Subcategoria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subcategoria");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
