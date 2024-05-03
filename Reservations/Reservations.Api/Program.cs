using Convey;
using Convey.WebApi;
using Reservations.Infrastructure;
using Reservations.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddInfrastructure()
    .AddApplication();

// Add services to the container.   
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UserInfrastructure();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
