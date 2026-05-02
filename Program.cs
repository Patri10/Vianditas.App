using Microsoft.EntityFrameworkCore;
using Npgsql;
using Vianditas.Data;
using Vianditas.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// ── Swagger / OpenAPI ──────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// ── PostgreSQL / EF Core ───────────────────────────────────────────────────────
var connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? ConvertDatabaseUrl(Environment.GetEnvironmentVariable("DATABASE_URL"))
    ?? ConvertDatabaseUrl(builder.Configuration.GetConnectionString("DefaultConnection"));

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se encontró la cadena de conexión. Configura ConnectionStrings__DefaultConnection o DATABASE_URL.");
}

builder.Services.AddDbContext<ViandistasDbContext>(options =>
    options.UseNpgsql(connectionString));

// ── CORS (para Evolution API / n8n) ───────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ── Middleware ─────────────────────────────────────────────────────────────────
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// ── Endpoints ─────────────────────────────────────────────────────────────────
app.MapMenuEndpoints();
app.MapPedidoEndpoints();
app.MapUsuarioEndpoints();
app.MapCategoriaEndpoints();
app.MapComercioEndpoints();
app.MapPagoEndpoints();

// ── Health check básico ────────────────────────────────────────────────────────
app.MapGet("/health", () => Results.Ok(new { status = "OK", timestamp = DateTime.UtcNow }))
    .WithTags("Health")
    .WithName("HealthCheck")
    .WithSummary("Verifica que la API esté activa");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ViandistasDbContext>();
    db.Database.Migrate();
}

app.Run();

// ── Helpers ───────────────────────────────────────────────────────────────────
static string? ConvertDatabaseUrl(string? databaseUrl)
{
    if (string.IsNullOrWhiteSpace(databaseUrl))
        return null;

    if (!databaseUrl.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)
        && !databaseUrl.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
        return databaseUrl;

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
        SslMode = SslMode.Disable,
        Pooling = false,
        KeepAlive = 1
    };

    return csBuilder.ConnectionString;
}


