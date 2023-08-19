using System.Text.Json.Serialization;
using CitWebApi.DB;
using CitWebApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Face to Face Web API",
        Description = "An ASP.NET Core Web API for managing student attendance in a smart way."
    });
});
builder.Services.AddDbContext<ApiContext>();
builder.Services.AddSingleton<IEncryptor, Encryptor>();
builder.Services.AddScoped<IAccessTokenValidator, AccessTokenValidator>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

Console.WriteLine("""

 /$$$$$$ /$$$$$$        /$$      /$$         /$$              /$$$$$$ /$$$$$$$ /$$$$$$
|_  $$_//$$__  $$      | $$  /$ | $$        | $$             /$$__  $| $$__  $|_  $$_/
  | $$ | $$  \ $$      | $$ /$$$| $$ /$$$$$$| $$$$$$$       | $$  \ $| $$  \ $$ | $$  
  | $$ | $$$$$$$$      | $$/$$ $$ $$/$$__  $| $$__  $$      | $$$$$$$| $$$$$$$/ | $$  
  | $$ | $$__  $$      | $$$$_  $$$| $$$$$$$| $$  \ $$      | $$__  $| $$____/  | $$  
  | $$ | $$  | $$      | $$$/ \  $$| $$_____| $$  | $$      | $$  | $| $$       | $$  
 /$$$$$| $$  | $$      | $$/   \  $|  $$$$$$| $$$$$$$/      | $$  | $| $$      /$$$$$$
|______|__/  |__/      |__/     \__/\_______|_______/       |__/  |__|__/     |______/
                                                                                                                                                                                                                                                 
                                                                                                                                                 
Developed by Barani Kumar S - Department of Mechatronics - CIT
""");

app.UseAuthorization();

app.MapControllers();

app.Run();
