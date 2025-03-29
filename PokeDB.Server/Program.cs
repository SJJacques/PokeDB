using Microsoft.EntityFrameworkCore;
using PokeDB.Server.Data;
using PokeDB.Server.Models.DTOs;
using PokeDB.Server.Services;
using PokeDB.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PokeDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<ICrudService<AbilityDto>, AbilityService>();
builder.Services.AddScoped<ICrudService<MoveDto>, MoveService>();
builder.Services.AddScoped<ICrudService<PokemonDto>, PokemonService>();
builder.Services.AddScoped<ICrudService<TypeDto>, TypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
