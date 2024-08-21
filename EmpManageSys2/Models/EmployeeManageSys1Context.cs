using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmpManageSys2.Models;

public partial class EmployeeManageSys1Context : DbContext
{
    public EmployeeManageSys1Context()
    {
    }

    public EmployeeManageSys1Context(DbContextOptions<EmployeeManageSys1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9PBATB6;Database=EmployeeManageSys1;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__City__7C36D07E84B0130E");

            entity.ToTable("City");

            entity.Property(e => e.RowId).HasColumnName("Row_Id");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__City__StateId__619B8048");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__Country__7C36D07EA999C95A");

            entity.ToTable("Country");

            entity.Property(e => e.RowId).HasColumnName("Row_Id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__Employee__7C36D07EF3AE79F4");

            entity.ToTable("EmployeeMaster");

            entity.HasIndex(e => e.MobileNumber, "UQ__Employee__250375B10A6A060D").IsUnique();

            entity.HasIndex(e => e.PassportNumber, "UQ__Employee__45809E714CA6C6B3").IsUnique();

            entity.HasIndex(e => e.EmailAddress, "UQ__Employee__49A14740FC073C00").IsUnique();

            entity.HasIndex(e => e.PanNumber, "UQ__Employee__7C38BFC803C9067D").IsUnique();

            entity.Property(e => e.RowId).HasColumnName("Row_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PanNumber)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImage).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.EmployeeMasters)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__EmployeeM__CityI__6A30C649");

            entity.HasOne(d => d.Contry).WithMany(p => p.EmployeeMasters)
                .HasForeignKey(d => d.ContryId)
                .HasConstraintName("FK__EmployeeM__Contr__68487DD7");

            entity.HasOne(d => d.State).WithMany(p => p.EmployeeMasters)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__EmployeeM__State__693CA210");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__State__7C36D07E0B6DC30A");

            entity.ToTable("State");

            entity.Property(e => e.RowId).HasColumnName("Row_Id");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__State__CountryId__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
