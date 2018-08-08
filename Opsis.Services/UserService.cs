using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;
using Opsis.Database.Context;
using Opsis.Interfaces;
using Opsis.Database.Entities.Management;
using Opsis.DTO;

namespace Opsis.Services
{
    public class UserService : IUserService
    {
        public LoggedOnUserDTO LoggedOnUser { get; private set; }

        public LoginState Login(string username, string password)
        {
            using (var db = new OpsisContext())
            {
                User user = db.Users.SingleOrDefault(u => u.Username == username);

                if (user != null && user.Password == BCryptHelper.HashPassword(password, user.Salt))
                {
                    if (IsBlocked(user))
                        return LoginState.Blocked;

                    LoggedOnUser = new LoggedOnUserDTO(user.Id, user.Username, user.Name, user.Surname, user.IsSystemUser);
                    GetPermissions(user);

                    return LoginState.LoggedOn;
                }
                return LoginState.WrongCredentials;
            }
        }

        public bool IsBlocked(User user)
            => user.UserStatusId != 1;

        private void GetPermissions(User user)
        {
            foreach (var r in user.UserRoles)
            {
                foreach (var p in r.Permissions)
                {
                    LoggedOnUser.PermissionIds.Add(p.Id);
                }
            }
        }
    }
}
