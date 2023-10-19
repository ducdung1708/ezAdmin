using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models.EntityModels;

namespace Models.DBContext;

public partial class ezSQLDBContext : DbContext
{
    public ezSQLDBContext(DbContextOptions<ezSQLDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetGroupUser> AspNetGroupUsers { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserSession> AspNetUserSessions { get; set; }

    public virtual DbSet<AspNetUserSite> AspNetUserSites { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LanguageKeyword> LanguageKeywords { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MenuDefine> MenuDefines { get; set; }

    public virtual DbSet<MenuDefineAspNetGroupUser> MenuDefineAspNetGroupUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetGroupUser>(entity =>
        {
            entity.HasKey(e => e.AspNetGroupUserId).HasName("PK__AspNetGroupUser");

            entity.Property(e => e.AspNetGroupUserId).ValueGeneratedNever();
        });

        modelBuilder.Entity<AspNetUserSession>(entity =>
        {
            entity.HasKey(e => e.AspNetUserSessionId).HasName("PK__AspNetUserSessions");

            entity.Property(e => e.AspNetUserSessionId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserSessions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUserSessions_AspNetUsers");
        });

        modelBuilder.Entity<AspNetUserSite>(entity =>
        {
            entity.Property(e => e.AspNetUserSiteId).ValueGeneratedNever();

            entity.HasOne(d => d.AspNetGroupUser).WithMany(p => p.AspNetUserSites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUserSites_AspNetGroupUser");

            entity.HasOne(d => d.Site).WithMany(p => p.AspNetUserSites).HasConstraintName("FK_AspNetUserSites_Sites");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserSites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUserSites_AspNetUsers");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cities_pk");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValueSql("((0))");
            entity.Property(e => e.SortIndex).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CountryCode).HasDefaultValueSql("('VN')");
            entity.Property(e => e.Status).HasDefaultValueSql("(N'Active')");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("District_pk");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValueSql("((0))");
            entity.Property(e => e.SortIndex).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.HasKey(e => e.KeywordId).HasName("PK__LocaleKeywordWords");

            entity.Property(e => e.KeywordId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageCode).HasName("PK_LocaleLanguage");
        });

        modelBuilder.Entity<LanguageKeyword>(entity =>
        {
            entity.HasKey(e => e.TranslateKeywordId).HasName("PK__LocaleTranslateKeyword");

            entity.Property(e => e.TranslateKeywordId).ValueGeneratedNever();
            entity.Property(e => e.TranslateKeyword).HasDefaultValueSql("('')");

            entity.HasOne(d => d.Keyword).WithMany(p => p.LanguageKeywords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LanguageKeywords_Keywords");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.LanguageKeywords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LanguageKeywords_Languages");
        });

        modelBuilder.Entity<MenuDefine>(entity =>
        {
            entity.HasKey(e => e.MenuCode).HasName("PK__MenuDefine");

            entity.Property(e => e.IsPublic).HasDefaultValueSql("((0))");
            entity.Property(e => e.Type).HasComment("Miền giá trị: PAGE, ACTION");
        });

        modelBuilder.Entity<MenuDefineAspNetGroupUser>(entity =>
        {
            entity.Property(e => e.MenuDefineAspNetGroupUserId).ValueGeneratedNever();

            entity.HasOne(d => d.AspNetGroupUser).WithMany(p => p.MenuDefineAspNetGroupUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuDefineAspNetGroupUsers_AspNetGroupUser");

            entity.HasOne(d => d.MenuCodeNavigation).WithMany(p => p.MenuDefineAspNetGroupUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuDefineAspNetGroupUsers_MenuDefine");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
