using Microsoft.EntityFrameworkCore;
using restaurant.api.ApplicationDb;
using restaurant.api.Endpoints;
using restaurant.api.MigrationExtension;
using restaurant.api.Services;
using user.api.Abstractions.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IMenuItemsService, MenuItemService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrate();
}

app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();
