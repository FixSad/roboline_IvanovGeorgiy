using Microsoft.EntityFrameworkCore;
using ProductCategory.DAL;
using ProductCategory.DAL.Interfaces;
using ProductCategory.DAL.Repositories;
using ProductCategory.Domain.Entities;
using ProductCategory.Service.Implementations;
using ProductCategory.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBaseRepository<ProductCategoryEntity>, ProductCategoryRepository>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

var connectionString = builder.Configuration.GetConnectionString("SQLite");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
