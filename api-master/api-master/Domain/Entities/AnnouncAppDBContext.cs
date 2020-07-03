using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static AnnouncApp.Domain.Interaction;

namespace AnnouncApp.Domain
{
    public partial class AnnouncAppDBContext : DbContext
    {
        public AnnouncAppDBContext()
        {
        }

        public AnnouncAppDBContext(DbContextOptions<AnnouncAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Interaction> Interaction { get; set; }
        public virtual DbSet<Recommendation> Recommendation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Announcements)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Announcement)
                .WithMany(a => a.Likes)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes);

            modelBuilder.Entity<Interaction>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interactions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Interaction>()
                .HasOne(i => i.Announcement)
                .WithMany(a => a.Interactions);

            var converter = new EnumToStringConverter<InteractionType>();

            modelBuilder.Entity<Interaction>()
                .Property(ui => ui.Type)
                .HasConversion(converter);

            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recommendations)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Announcement)
                .WithMany(a => a.Recommendations);
        }
    }
}
