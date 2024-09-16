using Microsoft.EntityFrameworkCore;
using Products.backend.Data;
using Products.backend.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbExt();
builder.AddConfigExt();
builder.Services.AddRepoExt();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();
app.Run();
