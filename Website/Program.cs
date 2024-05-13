using CoreWebApis.Modules.Home.BL.Classes;
using CoreWebApis.Modules.Home.BL.Interfaces;
using CoreWebApis.Modules.Home.Model.Classes;
using CoreWebApis.Modules.Home.Model.Interfaces;
using CoreWebApis.Modules.Login.BL.Classes;
using CoreWebApis.Modules.Login.BL.Interfaces;
using CoreWebApis.Modules.Login.DL.Classes;
using CoreWebApis.Modules.Login.DL.Interfaces;
using CoreWebApis.Modules.Login.Model.Classes;
using CoreWebApis.Modules.Login.Model.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Website;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICustomer, Customer>();
builder.Services.AddScoped<ICustomerBL, CustomerBL>();
builder.Services.AddScoped<CoreWebApis.Modules.Home.BL.Interfaces.IHomeViewModel, CoreWebApis.Modules.Home.BL.Classes.HomeBaseViewModel>();

//LoginResister
builder.Services.AddScoped<IRegisterAndLoginBL, RegisterAndLoginBL>();
builder.Services.AddScoped<CoreWebApis.Modules.Login.BL.Interfaces.IRegisterAndLoginViewModel, CoreWebApis.Modules.Login.BL.Classes.RegisterAndLoginBaseViewModel>();
builder.Services.AddScoped<IPgsqlRegisterAndLoginDL, PgsqlRegisterAndLoginDL>();
builder.Services.AddScoped<IUser,User>();

await builder.Build().RunAsync();
