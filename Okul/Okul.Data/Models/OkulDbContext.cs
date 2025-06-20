using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Okul.Data.Models;

public partial class OkulDbContext : DbContext
{
    public OkulDbContext()
    {
    }

    public OkulDbContext(DbContextOptions<OkulDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bolumler> Bolumlers { get; set; }

    public virtual DbSet<Dersler> Derslers { get; set; }

    public virtual DbSet<Notlar> Notlars { get; set; }

    public virtual DbSet<Ogrenciler> Ogrencilers { get; set; }

    public virtual DbSet<Ogretmenler> Ogretmenlers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=OkulDb;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bolumler>(entity =>
        {
            entity.HasKey(e => e.BolumId).HasName("PK__Bolumler__7BAD4B3C20DEB04A");

            entity.ToTable("Bolumler");

            entity.Property(e => e.BolumAdi).HasMaxLength(100);
        });

        modelBuilder.Entity<Dersler>(entity =>
        {
            entity.HasKey(e => e.DersId).HasName("PK__Dersler__E8B3DE11752081D0");

            entity.ToTable("Dersler");

            entity.Property(e => e.DersAdi).HasMaxLength(100);

            entity.HasOne(d => d.Ogretmen).WithMany(p => p.Derslers)
                .HasForeignKey(d => d.OgretmenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ders_Ogretmen");
        });

        modelBuilder.Entity<Notlar>(entity =>
        {
            entity.HasKey(e => e.NotId).HasName("PK__Notlar__4FB2008AE392090C");

            entity.ToTable("Notlar");

            entity.HasOne(d => d.Ders).WithMany(p => p.Notlars)
                .HasForeignKey(d => d.DersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Not_Ders");

            entity.HasOne(d => d.Ogrenci).WithMany(p => p.Notlars)
                .HasForeignKey(d => d.OgrenciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Not_Ogrenci");
        });

        modelBuilder.Entity<Ogrenciler>(entity =>
        {
            entity.HasKey(e => e.OgrenciId).HasName("PK__Ogrencil__E497E6B473445770");

            entity.ToTable("Ogrenciler");

            entity.Property(e => e.AdSoyad).HasMaxLength(100);
            entity.Property(e => e.Eposta).HasMaxLength(100);

            entity.HasOne(d => d.Bolum).WithMany(p => p.Ogrencilers)
                .HasForeignKey(d => d.BolumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ogrenci_Bolum");
        });

        modelBuilder.Entity<Ogretmenler>(entity =>
        {
            entity.HasKey(e => e.OgretmenId).HasName("PK__Ogretmen__27A3276F1FDFE62B");

            entity.ToTable("Ogretmenler");

            entity.Property(e => e.AdSoyad).HasMaxLength(100);
            entity.Property(e => e.Eposta).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
