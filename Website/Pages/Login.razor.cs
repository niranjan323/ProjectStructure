using CoreWebApis.Modules.Login.Model.Classes;
using CoreWebApis.Modules.Login.Model.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;

namespace Website.Pages
{
    partial class Login
    {
        private string activeTab = "login";
        public string ConfirmPassword { get; set; } = "";
        public bool showToast { get; set; } = false;
        public IUser User { get; set; } = new User();
        protected override void OnInitialized()
        {
            base.OnInitialized();
            User.saltcode = "";
            User.hashedpassword = "";
            User.email = "";
        }

        private void SetActiveTab(string tab)
        {
            activeTab = tab;
            StateHasChanged();
        }

        private async Task HandleLogin()
        {
            string token = await _viewmodel.UserLogin(User);
            if (token != null)
            {
                User = new User();
                SetJwtTokenCookie("jwtToken", token, TimeSpan.FromDays(1));
                _navigationManager.NavigateTo("Home");
            }
            else
            {
                // Handle login failure TimeSpan.FromMinutes(2)
            }
        }
        //private void SetJwtTokenCookie(string cookieName, string tokenValue, TimeSpan expiration)
        //{
        //    var expires = DateTime.UtcNow.Add(expiration);
        //    var cookieOptions = new CookieOptions
        //    {
        //        Expires = expires,
        //        SameSite = SameSiteMode.Strict,
        //        Secure = true, // Set to true if using HTTPS
        //        HttpOnly = true // Cookie cannot be accessed by client-side script
        //    };

        //    // Format the cookie string
        //    var cookie = $"{cookieName}={tokenValue};expires={expires.ToString("R")};path=/;samesite=strict;secure;httponly";

        //    // Set the cookie header in HttpClient
        //    _httpClient.DefaultRequestHeaders.Add("Set-Cookie", cookie);
        //}
        private async Task SetJwtTokenCookie(string cookieName, string tokenValue, TimeSpan expiration)
        {
            await _jSRuntime.InvokeVoidAsync("document.cookie", $"{cookieName}={tokenValue}; expires={DateTime.UtcNow.Add(expiration).ToString("R")}; path=/");
        }
        private async Task HandleRegister()
        {
            bool samePassword = User.Password == ConfirmPassword;
            if (samePassword)
            {
                bool result = await _viewmodel.UserRegister(User);
                if (result)
                {
                    activeTab = "login";
                    StateHasChanged();
                    User = new User();
                    ConfirmPassword = "";
                }
            }
            else
            {
                showToast = true;
                StateHasChanged();
            }
        }
    }
}
