using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;
using Opsis.Interfaces;
using Opsis.Database.Context;
using Opsis.Database.Entities.Db;
using Opsis.Database.Entities.Management;

namespace Opsis.Services
{
    public class DatabaseService : IDatabaseService
    {
        public void Initialize()
        {
            if (IsEmpty())
            {
                using (var db = new OpsisContext())
                {
                    InsertInitialRecord(db);
                    InsertUserStatuses(db);
                    InsertUserRoles(db);
                    InsertAdminUser(db);
                }
            }
        }

        public bool IsEmpty()
        {
            using (var db = new OpsisContext())
            {
                return db.VersionHistories.Count() == 0;
            }
        }

        public string GetCurrentVersionNumber()
        {
            using (var db = new OpsisContext())
            {
                return db.VersionHistories
                    .OrderByDescending(v => v.Id)
                    .Select(v => v.VersionNumber)
                    .SingleOrDefault();
            }
        }

        private void InsertInitialRecord(OpsisContext db)
        {
            var init = new VersionHistory
            {
                Id = 1,
                InitDate = DateTime.Now,
                VersionNumber = "1.0"
            };

            db.VersionHistories.Add(init);
            db.SaveChanges();
        }

        private void InsertUserStatuses(OpsisContext db)
        {
            var statuses = new List<UserStatus>
            {
                new UserStatus{ Id = 1, Name = Constants.UserStatusActive },
                new UserStatus{ Id = 2, Name = Constants.UserStatusBlocked }
            };

            foreach (var s in statuses)
            {
                db.UserStatuses.Add(s);
            }
            db.SaveChanges();
        }

        private void InsertUserRoles(OpsisContext db)
        {
            var roles = new List<UserRole>
            {
                new UserRole{ Id = 1, Name = Constants.AdminRoleName, IsSystemRole = true },
                new UserRole{ Id = 2, Name = Constants.UserRoleName, IsSystemRole = true }
            };

            foreach (var r in roles)
            {
                db.UserRoles.Add(r);
            }
            db.SaveChanges();
        }

        private void InsertAdminUser(OpsisContext db)
        {
            var salt = BCryptHelper.GenerateSalt();
            var password = BCryptHelper.HashPassword(Constants.AdminUserDefaultPassword, salt);

            var user = new User
            {
                Id = 1,
                Username = Constants.AdminUsername,
                Password = password,
                Salt = salt,
                IsSystemUser = true,
                Name = Constants.AdminName,
                Surname = Constants.AdminSurname,
                UserStatus = db.UserStatuses.SingleOrDefault(s => s.Id == 1),
                UserRoles = db.UserRoles.Where(r => r.Id == 1).ToList()
            };

            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
