using CoreWebApis.Models;
using CoreWebApis.Modules.Home.DL.Classes;
using CoreWebApis.Modules.Login.DL.Classes;
using CoreWebApis.Modules.Login.Model.Classes;
using CoreWebApis.Modules.Login.Model.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PgsqlRegisterAndLoginDL _pgsqlRegisterAndLoginDL;
        public LoginController(IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("PostgreSQLLocal");
            _pgsqlRegisterAndLoginDL = new PgsqlRegisterAndLoginDL(connection);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            bool result = await _pgsqlRegisterAndLoginDL.RegisterUserAsync(model.username,model.email, model.Password);

            if (result)
            {
                return Ok("User registered successfully.");
            }
            else
            {
                return BadRequest("Failed to register user.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            string token = await _pgsqlRegisterAndLoginDL.LoginAsync(model.username, model.Password);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }
    }
}

