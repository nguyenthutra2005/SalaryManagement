using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Interfaces;
using SalaryManagement.Application.Services;
using SalaryManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.IgnoreAntiforgeryTokenAttribute());
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITinhThueService, TinhThueService>();
builder.Services.AddScoped<IBaoHiemService, BaoHiemService>();
builder.Services.AddScoped<IBangLuongService, BangLuongService>();
builder.Services.AddScoped<IBaoCaoService, BaoCaoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var retries = 10;
    while (retries > 0)
    {
        try
        {
            Console.WriteLine("Dang ket noi database...");
            dbContext.Database.Migrate();
            Console.WriteLine("Migration thanh cong!");
            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"Thu lai... Con {retries} lan. Loi: {ex.Message}");
            Thread.Sleep(5000);
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();