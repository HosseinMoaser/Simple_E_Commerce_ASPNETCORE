using Microsoft.EntityFrameworkCore;
using Simple_E_Commerce.Data.Context;
using Simple_E_Commerce.Data.Repositories;
using Simple_E_Commerce.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Add DbContext
builder.Services.AddDbContext<SimpleEcommerceDbContext>(options =>
{
    options.UseSqlServer("Data Source = . ; Initial Catalog = SimpleEcommerceDB; Integrated Security = true");
});
#endregion

#region IoC

builder.Services.AddScoped<IGroupRepository, GroupRepository>();

#endregion

var app = builder.Build();

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
