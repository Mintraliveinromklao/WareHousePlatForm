using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSqlServer<warehouseContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Configuration.GetSection("UserAccount").Bind(AppSetting.Account);
builder.Configuration.GetSection("AppSettingConfig").Bind(AppSetting.Config);
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.Cookie.Name = "minnion";
    option.IdleTimeout = TimeSpan.FromDays(1);
    option.Cookie.IsEssential = true;
    option.Cookie.HttpOnly = true;
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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
