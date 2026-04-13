using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Vianditas.Application.Categorias.Services;
using Vianditas.Application.Comercios.Services;
using Vianditas.Application.Menus.Contract;
using Vianditas.Application.Menus.Services;
using Vianditas.Application.Menus.infrastructure;
using Vianditas.Application.Usuarios.infrastructure;
using Vianditas.Application.Usuarios.Contract;
using Vianditas.Application.Usuarios.Services;
using Vianditas.Data;


var builder = WebApplication.CreateBuilder(args);

// Registrar FluentValidation - Auto-descubre todos los validadores en el assembly actual
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddOpenApi();

var connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? ConvertDatabaseUrl(Environment.GetEnvironmentVariable("DATABASE_URL"))
    ?? ConvertDatabaseUrl(builder.Configuration.GetConnectionString("DefaultConnection"));

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se encontro la cadena de conexion. Configura ConnectionStrings__DefaultConnection o DATABASE_URL.");
}

// Configurar DbContext con PostgreSQL
builder.Services.AddDbContext<ViandistasDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar servicios y repositorios de aplicación
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IComercioService, ComercioService>();

// Agregar soporte para controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


// Mapear controladores
app.MapControllers();

app.Run();

static string? ConvertDatabaseUrl(string? databaseUrl)
{
    if (string.IsNullOrWhiteSpace(databaseUrl))
    {
        return null;
    }

    if (!databaseUrl.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)
        && !databaseUrl.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
    {
        return databaseUrl;
    }

    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);
    var username = userInfo.Length > 0 ? Uri.UnescapeDataString(userInfo[0]) : string.Empty;
    var password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty;

    var csBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Database = uri.AbsolutePath.TrimStart('/'),
        Username = username,
        Password = password,
        SslMode = SslMode.Require
    };

    return csBuilder.ConnectionString;
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
