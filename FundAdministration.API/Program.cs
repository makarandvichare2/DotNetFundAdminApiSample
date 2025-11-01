using FundAdministration.API.Extensions;
using FundAdministration.API.Helpers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices();

var app = builder.Build();
app.UseAndMapMiddleWares();

await DBHelpers.SeedDatabase(app);

Log.Information("Starting Fund Administration API");

app.Run();

public partial class Program { }