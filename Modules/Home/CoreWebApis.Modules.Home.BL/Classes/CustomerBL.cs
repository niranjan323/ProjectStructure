using CoreWebApis.Modules.Home.BL.Interfaces;
using CoreWebApis.Modules.Home.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Home.BL.Classes
{
    public class CustomerBL : ICustomerBL
    {
        public readonly CoreWebApis.Modules.Home.DL.Interfaces.IPGSQLCustomer? pGSQLCustomer;
        public Task<List<ICustomer>> GetEmployees()
        {
            return pGSQLCustomer.GetEmployees();
        }

    }
}

