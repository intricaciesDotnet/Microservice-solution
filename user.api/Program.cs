using Microsoft.EntityFrameworkCore;
using user.api.Abstractions.Interfaces;
using user.api.ApplicationDb;
using user.api.Endpoints;
using user.api.MigrationExtension;
using user.api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrate();
}

app.UseHttpsRedirection();
app.MapUserEndpoint();
app.Run();
