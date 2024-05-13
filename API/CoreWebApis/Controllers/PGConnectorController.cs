using CoreWebApis.Modules.Home.DL.Classes;
using CoreWebApis.Modules.Home.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PGConnectorController : ControllerBase
    {
        private readonly PGSQLCustomer _pgConnector;
        public PGConnectorController(IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("PGSQLLocal");
            _pgConnector = new PGSQLCustomer(connection);
        }

        
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                List<ICustomer> Employees = await _pgConnector.GetEmployees();
                if(Employees.Count == 0)
                {
                    throw new Exception();
                }
                else
                {
                    return Ok(Employees.Cast<CoreWebApis.Modules.Home.Model.Classes.Customer>().ToList());
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

    }
}
