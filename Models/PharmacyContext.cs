using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Final.Models;

public partial class PharmacyContext : DbContext
{
    public PharmacyContext()
    {
    }

    public PharmacyContext(DbContextOptions<PharmacyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Pharmacy;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PK__Branches__C6D111C93FD8E9E3");

            entity.Property(e => e.Bid).ValueGeneratedNever();
            entity.Property(e => e.Baddress)
                .HasMaxLength(90)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medicine__3214EC071166D7CF");

            entity.ToTable("Medicine");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Ingredient)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Mname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.BidNavigation).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.Bid)
                .HasConstraintName("FK__Medicine__Bid__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
