using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoreWebApis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly PostgresConnector _postgresConnector;

    public TestController(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgreSQLLocal");
        _postgresConnector = new PostgresConnector(connectionString);
    }


    [HttpGet(Name = "GetUsers")]
    public IActionResult Get()
    {
        var users = _postgresConnector.GetUsers();
        if (users.Result.Count == 0)
        {
            throw new Exception();
        }
        else
        {
            return Ok(users);
        }
    }

}
