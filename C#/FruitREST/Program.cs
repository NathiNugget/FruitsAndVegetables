using FruitClassLib;
using FruitREST;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#if DEBUG
TestMode.TestModeIsDev = true;
#endif


// TODO: Add singleton and create interface
builder.Services.AddSingleton<IReadingsRepository>(new ReadingsDB(TestMode.TestModeIsDev));
builder.Services.AddSingleton<IFoodDB>(new FoodDB(TestMode.TestModeIsDev));
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("GETpolicy", pol =>
    {
        pol.AllowAnyHeader().AllowAnyOrigin().WithMethods("GET");
    });
    // Policy
    opts.AddPolicy("PrivilegedPolicy", newpol =>
    {
        newpol.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
}); 

var app = builder.Build();






app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("GETpolicy");
app.UseCors("PrivilegedPolicy");



app.UseAuthorization();

app.MapControllers();


app.Run();
