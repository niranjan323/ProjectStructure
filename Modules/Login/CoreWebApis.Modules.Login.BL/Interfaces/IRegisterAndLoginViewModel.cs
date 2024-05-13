using CoreWebApis.Modules.Login.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Login.BL.Interfaces
{
    public interface IRegisterAndLoginViewModel
    {
        Task<bool> UserRegister(IUser details);
        Task<string> UserLogin(IUser user);
    }
}
