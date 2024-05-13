using CoreWebApis.Modules.Home.Model.Classes;
using CoreWebApis.Modules.Home.Model.Interfaces;

namespace Website.Pages
{
    partial class Home
    {
        public List<Customer> customers { get; set; }
        public object Response { get; set; }
        public bool IsInitialised { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected async override Task OnInitializedAsync()
        {
            if(_viewmodel != null)
            {
                customers = await _viewmodel.GetCustomers();
                Response = customers;
                IsInitialised = true;
                StateHasChanged();
            }
        }
    }
}
