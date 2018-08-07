﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opsis.Database.Entities.Management
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsSystemUser { get; set; }

        public int UserStatusId { get; set; }

        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
