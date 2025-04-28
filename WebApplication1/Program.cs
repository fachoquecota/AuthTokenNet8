using Validaciones.Business;
using Validaciones.Data.StoreProcedures;
using Validaciones.Interfaces;
using Validaciones.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Validaciones.Data;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde .env


// Configurar la carga de configuración desde appsettings.json y variables de entorno
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(); // Agregar soporte para variables de entorno

// Registrar servicios
builder.Services.AddScoped<ISprBusiness, StoredProcedureUsers>();
builder.Services.AddScoped<IBusinessLogin, BusinessLogin>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ISprProducts, StoredProcedureProducts>();
builder.Services.AddScoped<IProducts, BusinessProducts>();

builder.Services.AddSingleton<DbConnection>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar autenticación JWT usando variables de entorno
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Depuración: Imprime las variables capturadas
Console.WriteLine("=== VARIABLES CAPTURADAS ===");
Console.WriteLine($"ConnectionStrings:CadenaSQL: {builder.Configuration["ConnectionStrings:CadenaSQL"]}");
Console.WriteLine($"JWT:Key: {builder.Configuration["JWT:Key"]}");

// Muestra todas las variables de entorno cargadas
Console.WriteLine("\n=== VARIABLES DE ENTORNO ===");
foreach (var envVar in Environment.GetEnvironmentVariables())
{
    var entry = (System.Collections.DictionaryEntry)envVar;
    Console.WriteLine($"{entry.Key}: {entry.Value}");
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();