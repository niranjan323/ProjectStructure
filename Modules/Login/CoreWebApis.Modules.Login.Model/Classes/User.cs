using CoreWebApis.Modules.Login.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Login.Model.Classes
{
    public class User : IUser
    {
        public string username { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        public string saltcode { get; set; }
        public string hashedpassword { get; set; }
    }
}
