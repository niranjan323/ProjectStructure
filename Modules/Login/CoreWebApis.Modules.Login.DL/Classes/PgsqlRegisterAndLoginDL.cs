using CoreWebApis.Modules.Login.DL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CoreWebApis.Modules.Login.DL.Classes
{
    public class PgsqlRegisterAndLoginDL : IPgsqlRegisterAndLoginDL
    {
        private readonly string _connectionString;

        public PgsqlRegisterAndLoginDL(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<bool> RegisterUserAsync(string username, string email, string password)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) {
                await connection.OpenAsync();

                byte[] salt = GenerateSalt();
                byte[] hashedPassword = HashPassword(password, salt);
                string saltBase64 = Convert.ToBase64String(salt);
                string hashedPasswordBase64 = Convert.ToBase64String(hashedPassword);

                // Insert user details into database
                using (var cmd = new NpgsqlCommand("INSERT INTO users (username,email, saltcode, hashedpassword) VALUES (@Username,@email, @Salt, @Hash)", connection))
                {
                    cmd.Parameters.AddWithValue("Username", username);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("Salt", saltBase64);
                    cmd.Parameters.AddWithValue("Hash", hashedPasswordBase64);

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
                
        }
    public async Task<string> LoginAsync(string username, string password)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();

           
            using (var cmd = new NpgsqlCommand("SELECT saltcode, hashedpassword FROM users WHERE username = @Username", connection))
            {
                cmd.Parameters.AddWithValue("Username", username);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                       
                        string saltBase64 = reader.GetString(0);
                        string hashedPasswordBase64 = reader.GetString(1);

                        
                        byte[] salt = Convert.FromBase64String(saltBase64);
                        byte[] hashedPassword = Convert.FromBase64String(hashedPasswordBase64);

                        
                        byte[] hashedPasswordAttempt = HashPassword(password, salt);

                        // Compare hashed passwords
                        if (hashedPassword.SequenceEqual(hashedPasswordAttempt))
                        {

                            string token = GenerateJwtToken(username); 
                            return token;
                        }
                    }
                }
            }
        }

        return null;
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private byte[] HashPassword(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000)) 
        {
            return pbkdf2.GetBytes(32); 
        }
    }

        private string GenerateJwtToken(string username)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(username)); 
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Name, username),
            //    // Add more claims as needed, e.g., roles, etc.
            //};

            //var token = new JwtSecurityToken(
            //    issuer: "Niranjan", 
            //    audience: "Niranjan", 
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(1), // Token expiration time
            //    signingCredentials: credentials
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);
            return "dghjkesjdbksfjbskdfvjdvfskdjfbksjfbksdfjbf";
        }




    }
}

