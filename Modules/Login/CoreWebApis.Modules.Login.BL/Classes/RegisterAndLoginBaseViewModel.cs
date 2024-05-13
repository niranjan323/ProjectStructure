using CoreWebApis.Modules.Login.BL.Interfaces;
using CoreWebApis.Modules.Login.Model.Classes;
using CoreWebApis.Modules.Login.Model.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Login.BL.Classes
{
    public class RegisterAndLoginBaseViewModel : IRegisterAndLoginViewModel
    {
        public readonly HttpClient _httpClient;
        public RegisterAndLoginBaseViewModel(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }
        public async Task<bool> UserRegister(IUser details)
        {
            try
            {
                string jsonDetails = JsonConvert.SerializeObject(details);

                // Create StringContent with JSON data
                StringContent content = new StringContent(jsonDetails, Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7194/api/Login/register", content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to register user: {response.StatusCode}. Error message: {errorMessage}");
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<string> UserLogin(IUser user)
        {
            try
            {
                string jsonDetails = JsonConvert.SerializeObject(user);

                // Create StringContent with JSON data
                StringContent content = new StringContent(jsonDetails, Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7194/api/Login/login", content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    return token;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to Login user: {response.StatusCode}. Error message: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to login user.", ex);
            }
        }
    }
}
