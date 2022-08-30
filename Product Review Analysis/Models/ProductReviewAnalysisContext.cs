using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Product_Review_Analysis.Models
{
    public partial class ProductReviewAnalysisContext : DbContext
    {
        public ProductReviewAnalysisContext()
        {
        }

        public ProductReviewAnalysisContext(DbContextOptions<ProductReviewAnalysisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Compare> Compares { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Rateing> Rateings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Product Review Analysis;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Photo).HasColumnName("photo");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripition).HasColumnName("descripition");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_comment_user");
            });

            modelBuilder.Entity<Compare>(entity =>
            {
                entity.ToTable("compare");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Compares)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_compare_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Compares)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_compare_user");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_product_category");
            });

            modelBuilder.Entity<Rateing>(entity =>
            {
                entity.ToTable("rateing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rateings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_rateing_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adress)
                    .HasMaxLength(50)
                    .HasColumnName("adress");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
