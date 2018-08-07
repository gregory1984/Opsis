using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opsis.Database.Entities.Management;
using Opsis.Database.Entities.Db;

namespace Opsis.Database.Context
{
    public class OpsisContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<UserStatus> UserStatuses { get; set; }
        public IDbSet<Permission> Permissions { get; set; }
        public IDbSet<VersionHistory> VersionHistories { get; set; }

        public OpsisContext() : base("name=Opsis")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapUser(modelBuilder);
            MapUserRole(modelBuilder);
            MapPermission(modelBuilder);
            MapVersionHistory(modelBuilder);
        }

        private void MapPermission(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasKey(g => g.Id)
                .Property(g => g.Name).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<Permission>()
                .HasMany(p => p.UserRoles)
                .WithMany(g => g.Permissions);
        }

        private void MapUserRole(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(r => r.Id)
                .Property(r => r.Name).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<UserRole>()
                .Property(r => r.IsSystemRole).IsRequired();

            modelBuilder.Entity<UserRole>()
                .HasMany(r => r.Users)
                .WithMany(u => u.UserRoles);

            modelBuilder.Entity<UserRole>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.UserRoles);
        }

        private void MapUserStatus(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserStatus>()
                .HasKey(s => s.Id)
                .Property(s => s.Name).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<UserStatus>()
                .HasMany(s => s.Users)
                .WithRequired(u => u.UserStatus)
                .HasForeignKey(u => u.UserStatusId);
        }

        private void MapUser(DbModelBuilder modelBuilder)
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
                .Property(u => u.IsSystemUser).IsRequired();

            modelBuilder.Entity<User>()
                .HasRequired(u => u.UserStatus)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.UserStatusId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithMany(g => g.Users);
        }

        private void MapVersionHistory(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VersionHistory>()
                .HasKey(v => v.Id)
                .Property(v => v.VersionNumber).IsRequired();

            modelBuilder.Entity<VersionHistory>()
                .Property(v => v.InitDate).IsRequired();
        }
    }
}
