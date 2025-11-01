using FundAdministration.API.Extensions;
using FundAdministration.API.Helpers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.AddServices();

var app = builder.Build();
app.UseAndMapMiddleWares();

await DBHelpers.SeedDatabase(app, builder.Configuration);

Log.Information("Starting Fund Administration API");

app.Run();

public partial class Program { }