using CoreWebApis.Modules.Home.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Home.Model.Classes
{
    public class Customer : ICustomer
    {
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public int Age { get; set; }
    }
}
