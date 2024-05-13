using CoreWebApis.Modules.Login.BL.Interfaces;
using CoreWebApis.Modules.Login.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Login.BL.Classes
{
    public class RegisterAndLoginBL : IRegisterAndLoginBL
    {
        public readonly CoreWebApis.Modules.Login.DL.Interfaces.IPgsqlRegisterAndLoginDL _pgsqlRegisterAndLoginDL;
        public Task<bool> RegisterUserAsync(string username,string email, string password)
        {
            return _pgsqlRegisterAndLoginDL.RegisterUserAsync(username,email, password);
        }
        public Task<string> LoginAsync(string username, string password)
        {
            return _pgsqlRegisterAndLoginDL.LoginAsync(username, password);
        }
    }
}
