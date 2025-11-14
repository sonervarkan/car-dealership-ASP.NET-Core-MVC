using car_dealership_ASP.NET_Core_MVC.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session
builder.Services.AddSession();

var connectionString = builder.Configuration["DefaultConnection"];
builder.Services.AddDbContext<CarDealershipDbContext>(options =>
options.UseSqlServer(connectionString));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// SESSION ICIN (app.UseRouting() ile app.UseAuthorization()’ýn tam arasýnda olmalý.)
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
