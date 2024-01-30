using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HobbyCollection.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MainDbContext") ?? throw new InvalidOperationException("Connection string 'MainDbContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Logging setting
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
