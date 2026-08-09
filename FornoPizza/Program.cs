using FornoPizza.Data;
using FornoPizza.Data.Repository;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Services;
using FornoPizza.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultDbConnection");
builder.Services.AddDbContext<WebContext>(op => op.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
