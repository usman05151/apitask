using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Options;
using CleanArchApi.Infrastructure.Data;
using CleanArchApi.Domain.Interfaces;
using CleanArchApi.Infrastructure.Repositries;
using CleanArchApi.Application.Services;
using Microsoft.Extensions.Hosting.Internal;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

//conn string 
var Excestring = builder.Configuration["Exceptional:Store:ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(Options => Options.UseMySql(Excestring, new MySqlServerVersion(new Version(8, 0, 21))));
// exception handling 
builder.Services.AddExceptional(builder.Configuration.GetSection("Exceptional"), Options =>
{
    Options.UseExceptionalPageOnThrow = builder.Environment.IsDevelopment();
});

builder.Services.AddScoped<IProductRepository, ProductRepositry>();
// register the service
builder.Services.AddScoped<ProductService>();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptional();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
