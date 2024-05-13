using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Login.DL.Interfaces
{
    public interface IPgsqlRegisterAndLoginDL
    {
        Task<bool> RegisterUserAsync(string username,string email, string password);
        Task<string> LoginAsync(string username, string password);
    }
}
