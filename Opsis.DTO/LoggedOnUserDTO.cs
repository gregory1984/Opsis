using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opsis.DTO
{
    public class LoggedOnUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsSystemUser { get; set; }

        public ISet<int> PermissionIds { get; set; }

        public LoggedOnUserDTO(int id, string username, string name, string surname, bool isSystemUser)
        {
            Id = id;
            Username = username;
            Name = name;
            Surname = surname;
            IsSystemUser = isSystemUser;
            PermissionIds = new HashSet<int>();
        }
    }
}
