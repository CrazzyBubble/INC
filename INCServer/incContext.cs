using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INCServer
{
    public partial class incContext : DbContext
    {
        public incContext()
        {
        }

        public incContext(DbContextOptions<incContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<FilmGenre> FilmGenre { get; set; }
        public virtual DbSet<FilmResources> FilmResources { get; set; }
        public virtual DbSet<FilmStudio> FilmStudio { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<LastWatch> LastWatch { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Studios> Studios { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserRights> UserRights { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VideoFormat> VideoFormat { get; set; }
        public virtual DbSet<Watched> Watched { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=inc;Username=postgres;Password=SaNeK2020");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.Name)
                    .HasName("categories_unique_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.ToTable("countries");

                entity.HasIndex(e => e.Name)
                    .HasName("countries_unique_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<FilmGenre>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("film_genre");

                entity.HasIndex(e => new { e.Filmid, e.Genreid })
                    .HasName("film_genre_unique_filmgenre")
                    .IsUnique();

                entity.Property(e => e.Filmid).HasColumnName("filmid");

                entity.Property(e => e.Genreid).HasColumnName("genreid");

                entity.HasOne(d => d.Film)
                    .WithMany()
                    .HasForeignKey(d => d.Filmid)
                    .HasConstraintName("film_genre_fk_film");

                entity.HasOne(d => d.Genre)
                    .WithMany()
                    .HasForeignKey(d => d.Genreid)
                    .HasConstraintName("film_genre_fk_genre");
            });

            modelBuilder.Entity<FilmResources>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("film_resources");

                entity.Property(e => e.Filmid).HasColumnName("filmid");

                entity.Property(e => e.Formatid).HasColumnName("formatid");

                entity.Property(e => e.Src)
                    .IsRequired()
                    .HasColumnName("src")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Film)
                    .WithMany()
                    .HasForeignKey(d => d.Filmid)
                    .HasConstraintName("film_resources_fk_film");

                entity.HasOne(d => d.Format)
                    .WithMany()
                    .HasForeignKey(d => d.Formatid)
                    .HasConstraintName("film_resources_fk_format");
            });

            modelBuilder.Entity<FilmStudio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("film_studio");

                entity.HasIndex(e => new { e.Filmid, e.Studioid })
                    .HasName("film_studio_unique_filmstudio")
                    .IsUnique();

                entity.Property(e => e.Filmid).HasColumnName("filmid");

                entity.Property(e => e.Studioid).HasColumnName("studioid");

                entity.HasOne(d => d.Film)
                    .WithMany()
                    .HasForeignKey(d => d.Filmid)
                    .HasConstraintName("film_studio_fk_film");

                entity.HasOne(d => d.Studio)
                    .WithMany()
                    .HasForeignKey(d => d.Studioid)
                    .HasConstraintName("film_studio_fk_studio");
            });

            modelBuilder.Entity<Films>(entity =>
            {
                entity.ToTable("films");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoriesid).HasColumnName("categoriesid");

                entity.Property(e => e.Countryid).HasColumnName("countryid");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Directors)
                    .HasColumnName("directors")
                    .HasColumnType("character varying(30)[]");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Tags)
                    .HasColumnName("tags")
                    .HasColumnType("character varying(15)[]");

                entity.HasOne(d => d.Categories)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.Categoriesid)
                    .HasConstraintName("films_fk_categories");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.Countryid)
                    .HasConstraintName("films_fk_country");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.ToTable("genres");

                entity.HasIndex(e => e.Name)
                    .HasName("genres_unique_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<LastWatch>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("last_watch");

                entity.HasIndex(e => e.Userid)
                    .HasName("last_watch_unique_user")
                    .IsUnique();

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Watchedid).HasColumnName("watchedid");

                entity.HasOne(d => d.User)
                    .WithOne()
                    .HasForeignKey<LastWatch>(d => d.Userid)
                    .HasConstraintName("last_watch_fk_user");

                entity.HasOne(d => d.Watched)
                    .WithMany()
                    .HasForeignKey(d => d.Watchedid)
                    .HasConstraintName("last_watch_fk_watched");
            });

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("photos");

                entity.Property(e => e.Filmid).HasColumnName("filmid");

                entity.Property(e => e.Istitul)
                    .HasColumnName("istitul")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Src)
                    .IsRequired()
                    .HasColumnName("src")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Film)
                    .WithMany()
                    .HasForeignKey(d => d.Filmid)
                    .HasConstraintName("photos_fk_film");
            });

            modelBuilder.Entity<Studios>(entity =>
            {
                entity.ToTable("studios");

                entity.HasIndex(e => e.Name)
                    .HasName("studios_unique_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_info");

                entity.HasIndex(e => e.Userid)
                    .HasName("user_info_unique_user")
                    .IsUnique();

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(20);

                entity.Property(e => e.Imgsrc)
                    .HasColumnName("imgsrc")
                    .HasMaxLength(255);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(20);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.HasOne(d => d.User)
                    .WithOne()
                    .HasForeignKey<UserInfo>(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_info_fk_user");
            });

            modelBuilder.Entity<UserRights>(entity =>
            {
                entity.ToTable("user_rights");

                entity.HasIndex(e => e.Name)
                    .HasName("user_rights_unique_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("users_unique_email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(32);

                entity.Property(e => e.Rightid).HasColumnName("rightid");

                entity.Property(e => e.IsActive).HasColumnName("isactive");

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Rightid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_fk_rights");
            });

            modelBuilder.Entity<VideoFormat>(entity =>
            {
                entity.ToTable("video_format");

                entity.HasIndex(e => e.Name)
                    .HasName("video_format_unique_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Watched>(entity =>
            {
                entity.ToTable("watched");

                entity.HasIndex(e => new { e.Userid, e.Filmid })
                    .HasName("watched_unique_userfilm")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Filmid).HasColumnName("filmid");

                entity.Property(e => e.Islike)
                    .HasColumnName("islike")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Iswatched)
                    .HasColumnName("iswatched")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Timestop)
                    .HasColumnName("timestop")
                    .HasColumnType("time without time zone")
                    .HasDefaultValueSql("'00:00:00'::time without time zone");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Watched)
                    .HasForeignKey(d => d.Filmid)
                    .HasConstraintName("watched_fk_film");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Watched)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("watched_fk_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
