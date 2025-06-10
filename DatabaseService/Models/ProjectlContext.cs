using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService.Models;

public partial class ProjectlContext : DbContext
{
    public ProjectlContext()
    {
    }

    public ProjectlContext(DbContextOptions<ProjectlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:databaseserverforprojectl.database.windows.net,1433;Initial Catalog=projectl;Persist Security Info=False;User ID=sumaa;Password=Mikkey_22;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5486486C1BC68D");

            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AUDITLOGS_USERS");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207A16933A4");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA1DA561A6").IsUnique();

            entity.Property(e => e.Author)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__Issues__6C861604501DCE23");

            entity.HasOne(d => d.Book).WithMany(p => p.Issues)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUES_BOOKS");

            entity.HasOne(d => d.Member).WithMany(p => p.Issues)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUES_MEMBERS");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B18C457D77C");

            entity.HasIndex(e => e.Email, "UQ__Members__A9D105341AAC818B").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C4421AED7");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534FB268E8E").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Mfaenabled).HasColumnName("MFAEnabled");
            entity.Property(e => e.Mfasecret)
                .HasMaxLength(255)
                .HasColumnName("MFASecret");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
