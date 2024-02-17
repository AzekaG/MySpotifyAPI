using Microsoft.EntityFrameworkCore;
using MySpotify.BLL.Interfaces;
using MySpotify.BLL.Services;
using MySpotify.BLL.Infrastructure;






var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddMediaUserContext(connection);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IGenreService, GenreService>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



app.UseSession();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
