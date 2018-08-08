using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opsis.DTO;

namespace Opsis.Interfaces
{
    public enum LoginState
    {
        LoggedOn,
        WrongCredentials,
        Blocked
    }

    public interface IUserService
    {
        LoggedOnUserDTO LoggedOnUser { get; }
        LoginState Login(string username, string password);
    }
}
