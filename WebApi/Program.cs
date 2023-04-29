using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Repository.Interfaces;
using WebApi.Repository.PostgreSQL;
using WebApi.Services;
using WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .UseKestrel()
    .UseUrls("http://*:5021")
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration();

// Add services to the container.

builder.Services.AddControllers((options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgresDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<PostgresDBContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
