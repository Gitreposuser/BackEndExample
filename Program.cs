using Data.Contracts;
using Data.DbContextRealization;
using Host.Config.DependenciesRegistrator;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("ApplicationDatabase");

// Cors
var corsPolicy = "AllowOrigin";
builder.Services.AddCors(opts =>
    opts.AddPolicy(corsPolicy, options => 
        options.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IMangaShopDbContext, MangaShopDbContext>
    (x => x.UseSqlServer(connectionString, b => b.MigrationsAssembly("Host")));
DependencyRegistrator.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();