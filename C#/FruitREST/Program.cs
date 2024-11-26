using FruitClassLib;
using FruitREST;
using FruitREST.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#if DEBUG
TestMode.TestModeIsDev = true;
#endif

ReadingsDB _readingrepo = new ReadingsDB(TestMode.TestModeIsDev);
// TODO: Add singleton and create interface


var app = builder.Build();






app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
