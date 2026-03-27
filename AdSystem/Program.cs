using Microsoft.EntityFrameworkCore;
using AdSystem.Data;
using AdSystem.DAL;
using AdSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<SubscriberApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["SubscriberApi:BaseUrl"]!);
});

builder.Services.AddScoped<IAdvertiserRepository, AdvertiserRepository>();
builder.Services.AddScoped<IAdRepository, AdRepository>();
builder.Services.AddScoped<IAdService, AdService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
