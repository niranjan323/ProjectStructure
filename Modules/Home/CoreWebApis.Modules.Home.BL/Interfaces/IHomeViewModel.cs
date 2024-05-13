using CoreWebApis.Modules.Home.Model.Classes;
using CoreWebApis.Modules.Home.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Home.BL.Interfaces
{
    public interface IHomeViewModel
    {
        public List<CoreWebApis.Modules.Home.Model.Classes.Customer> cutomerListTostore { set; get; }
        Task<List<Customer>> GetCustomers();
    }
}
