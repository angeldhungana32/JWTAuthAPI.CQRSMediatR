using JWTAuthAPI.API.Middlewares;
using JWTAuthAPI.Application;
using JWTAuthAPI.Core;
using JWTAuthAPI.Infrastructure;
using JWTAuthAPI.Infrastructure.Data;
using JWTAuthAPI.Infrastructure.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddDebug();

builder.Services.AddControllers()
               .AddJsonOptions(options =>
                   options.JsonSerializerOptions
                   .Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    await SeedDatabaseAsync(app);
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseMyCorsPolicy();
app.UseJwtAuthorization();
app.MapControllers();
app.Run();

static async Task SeedDatabaseAsync(IHost app)
{
    using var scope = app.Services.CreateScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>();
    await initialiser.InitializeDBAsync();
    await initialiser.SeedDBAsync();
}