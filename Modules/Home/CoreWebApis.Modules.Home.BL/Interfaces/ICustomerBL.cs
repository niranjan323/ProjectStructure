using CoreWebApis.Modules.Home.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Home.BL.Interfaces
{
    public interface ICustomerBL
    {
        Task<List<ICustomer>> GetEmployees();
    }
}
