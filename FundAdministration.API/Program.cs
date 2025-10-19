using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.API.Middlewares;
using FundAdministration.API.Mocks;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure;
using FundAdministration.Infrastructure.Data;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Funds.Validators;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddControllers();

// mocking to authenticate endpoint successfully
builder.Services.AddAuthentication("Mock").AddScheme<AuthenticationSchemeOptions, MockAuthHandler>("Mock", null);
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile= $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
    options.IncludeXmlComments(xmlPath);
});

ConfigureMediatR();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); // Adds Strict-Transport-Security header
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //c.SwaggerEndpoint("v1/swagger.json", "Fund Administration API v1");
    });
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MapControllers();

await SeedDatabase(app);

app.Run();

static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
       // context.Database.Migrate(); // uncomment for creating the db on running the api
        context.Database.EnsureCreated();
        await SeedData.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

void ConfigureMediatR()
{
    var mediatRAssemblies = new[]
    {
        Assembly.GetAssembly(typeof(Fund)), // Core
        Assembly.GetAssembly(typeof(CreateFundCommand)), // UseCases
    };
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
    builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(CreateFundValidator)));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}
