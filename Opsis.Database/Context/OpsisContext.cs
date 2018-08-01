using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opsis.Database.Entities.Management;

namespace Opsis.Database.Context
{
    public class OpsisContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Permission> Permissions { get; set; }

        public OpsisContext() : base("name=Opsis")
        {
            //  TODO: encrypt connection string inside the app.config file.
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapUserEntity(modelBuilder);
            MapGroupEntity(modelBuilder);
            MapPermissionEntity(modelBuilder);
        }

        private void MapPermissionEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasKey(g => g.Id)
                .Property(g => g.Name).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<Permission>()
                .HasMany(p => p.Groups)
                .WithMany(g => g.Permissions);
        }

        private void MapGroupEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasKey(g => g.Id)
                .Property(g => g.Name).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(u => u.Groups);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Permissions)
                .WithMany(p => p.Groups);
        }

        private void MapUserEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id)
                .Property(u => u.Username).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password).HasMaxLength(512).IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Salt).HasMaxLength(512).IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Name).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Surname).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users);
        }
    }
}
