using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Home.Model.Interfaces
{
    public interface ICustomer
    {
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public int Age { get; set; }
    }
}
