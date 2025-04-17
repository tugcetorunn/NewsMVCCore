using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using MVCCore11IdentityUygulama.Data;
using MVCCore11IdentityUygulama.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 11. DbContext sınıfının service olarak eklenmesi
builder.Services.AddDbContext<HaberPortalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HaberDbConnection")));

// 12. identity için gerekli olan servislerin eklenmesi
builder.Services.AddIdentity<Editor, Rol>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<HaberPortalDbContext>().AddRoles<Rol>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// 14. migration işlemleri
// 13. authorize işlemleri için önce authenticate kullandırılmalı.
app.UseAuthentication();

app.UseAuthorization();

// 20. areaların oluşturulması
// 21. area route işlemleri
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
