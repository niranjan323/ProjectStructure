using CoreWebApis.Modules.Home.BL.Interfaces;
using CoreWebApis.Modules.Home.Model.Classes;
using CoreWebApis.Modules.Home.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Home.BL.Classes
{
    public class HomeBaseViewModel : IHomeViewModel
    {
        public readonly HttpClient _httpClient;
        public List<CoreWebApis.Modules.Home.Model.Classes.Customer> cutomerListTostore { set; get; }

        public HomeBaseViewModel(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                List<Customer> cutomerList = await _httpClient.GetFromJsonAsync<List<Customer>>("https://localhost:7194/api/PGConnector/GetEmployees");
                cutomerListTostore = cutomerList;
                return cutomerList;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"An error occurred while fetching customers: {ex.Message}");

                // Return a default value or null
                return null;
            }
        }
    }
}
