using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opsis.Interfaces
{
    public interface IDatabaseService
    {
        void Initialize();
        bool IsEmpty();
        string GetCurrentVersionNumber();
    }
}
