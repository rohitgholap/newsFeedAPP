using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NewsFeedApp.Models;

public partial class NewsFeedContext : DbContext
{
    public NewsFeedContext()
    {
    }

    public NewsFeedContext(DbContextOptions<NewsFeedContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("ConnectionStringGoesHere if NOT injected in the startup");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Article__3213E83FFEEF2F0F");

            entity.ToTable("Article");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.ArticleDate)
                .HasColumnType("datetime")
                .HasColumnName("article_date");
            entity.Property(e => e.ArticleId)
                .HasMaxLength(50)
                .HasColumnName("article_id");
            entity.Property(e => e.ArticleLink)
                .HasMaxLength(500)
                .HasColumnName("article_link");
            entity.Property(e => e.ArticleTitle)
                .HasMaxLength(500)
                .HasColumnName("article_title");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CorrelationId)
                .HasMaxLength(50)
                .HasColumnName("correlation_id");

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("fk_article_author");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3213E83FDB61F077");

            entity.ToTable("Author");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
