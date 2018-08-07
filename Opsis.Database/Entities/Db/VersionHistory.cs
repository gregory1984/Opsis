using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opsis.Database.Entities.Db
{
    public class VersionHistory
    {
        public int Id { get; set; }
        public string VersionNumber { get; set; }
        public DateTime InitDate { get; set; }
    }
}
