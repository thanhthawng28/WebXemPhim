using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebXemPhim.Models
{
    public partial class WebXemPhimContext : DbContext
    {
        public WebXemPhimContext()
        {
        }

        public WebXemPhimContext(DbContextOptions<WebXemPhimContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FavoriteMovie> FavoriteMovies { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieActor> MovieActors { get; set; } = null!;
        public virtual DbSet<MovieView> MovieViews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRating> UserRatings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WebXemPhim;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteMovie>(entity =>
            {
                entity.HasKey(e => e.FavoriteMoviesId)
                    .HasName("PK__Favorite__24079D0E7861EAD7");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.FavoriteMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FavoriteMovies_Movies");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.FavoriteMovies)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FavoriteMovies_Users");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.GenreName, "UQ__Genres__BBE1C339E69D5421")
                    .IsUnique();

                entity.Property(e => e.GenreName).HasMaxLength(30);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.DirectorName).HasMaxLength(50);

                entity.Property(e => e.Directory).HasColumnType("text");

                entity.Property(e => e.ImageLink).HasColumnType("text");

                entity.Property(e => e.MovieStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReleaseDay).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Movies_Genres");
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(e => e.MovieActorsId)
                    .HasName("PK__MovieAct__71A14400F4128B5E");

                entity.Property(e => e.ActorName).HasMaxLength(50);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieActors)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MovieActors_Movies");
            });

            modelBuilder.Entity<MovieView>(entity =>
            {
                entity.HasKey(e => e.MovieViewsId)
                    .HasName("PK__MovieVie__2C1E8DB5D6177497");

                entity.Property(e => e.TimeView).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieViews)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MovieViews_Movies");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.MovieViews)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MovieViews_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Users__C9F284575915088F");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.HasKey(e => e.UserRatingsId)
                    .HasName("PK__UserRati__4ACA2F4FC7A5CBD1");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserRatings_Movies");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserRatings_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
