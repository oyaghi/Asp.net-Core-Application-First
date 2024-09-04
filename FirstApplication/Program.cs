using FirstApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Database Config with lazy loading 
builder.Services.AddDbContext<TestingDbContext>(option=>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("conn")).UseLazyLoadingProxies();
});

// Session Config 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15); //Idel time before session dies 
    //options.Cookie.HttpOnly = true; //ensures that the cookie is accessible only to the server side and not available through client-side scripts (e.g., JavaScript).
    //options.Cookie.IsEssential = true; 

}
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Session Config
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");

app.Run();
