using Microsoft.EntityFrameworkCore;
using CMPG323API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Apply dependancy injection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<cmpg323projectdevContext>(options => options.UseSqlServer(connectionString)); 

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
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CMPG323API");
        options.RoutePrefix = string.Empty;
    });
}
    
app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

//IConfiguration configuration = app.Configuration;
//IWebHostEnvironment environment = app.Environment;

app.MapControllers();

app.Run();
