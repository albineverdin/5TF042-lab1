using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SubscriberSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<SubscriberSystem.DAL.ISubscriberRepository, SubscriberSystem.DAL.SubscriberRepository>();
builder.Services.AddScoped<SubscriberSystem.Services.ISubscriberService, SubscriberSystem.Services.SubscriberService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
