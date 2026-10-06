using FornoPizza.Data;
using FornoPizza.Endpoints;
using FornoPizza.Data.Repository;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Hubs;
using FornoPizza.Middleware;
using FornoPizza.Services;
using FornoPizza.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultDbConnection");
builder.Services.AddDbContext<WebContext>(op => op.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHostedService<OrderQueueBackgroundService>();
builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

builder.Services
    .AddAuthentication(AuthService.AUTH_KEY)
    .AddCookie(AuthService.AUTH_KEY, options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

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

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LocalizationMiddleware>();

app.MapHub<OrderHub>("/orderHub");
app.MapJsonApi();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
