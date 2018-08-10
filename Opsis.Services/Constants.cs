using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opsis.Services
{
    internal class Constants
    {
        public static string UserStatusActive { get; } = "Aktywny";
        public static string UserStatusBlocked { get; } = "Zablokowany";

        public static string UserRoleName { get; } = "Użytkownik";
        public static string AdminRoleName { get; } = "Administrator";

        public static string AdminUsername { get; } = "administrator";
        public static string AdminName { get; } = "Administrator";
        public static string AdminSurname { get; } = "";
        public static string AdminUserDefaultPassword { get; } = "administrator";
    }
}
