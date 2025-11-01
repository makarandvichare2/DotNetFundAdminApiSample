using Ardalis.SharedKernel;
using Asp.Versioning;
using FluentValidation;
using FundAdministration.API.Helpers;
using FundAdministration.API.Mocks;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Funds.Validators;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly.Caching;
using Polly.Caching.Memory;
using Serilog;
using System.Reflection;

namespace FundAdministration.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IAsyncCacheProvider, MemoryCacheProvider>();
            builder.Services.AddControllers();
            builder.Services.AddHealthChecks()
                .AddSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"),
                              name: "SQL Server",
                              healthQuery: "SELECT 1;",
                              failureStatus: HealthStatus.Unhealthy);
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // mocking to authenticate endpoint successfully
            builder.Services.AddAuthentication("Mock").AddScheme<AuthenticationSchemeOptions, MockAuthHandler>("Mock", null);
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            ConfigureMediatR(builder);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Host.UseSerilog();
        }

         private static void ConfigureMediatR(WebApplicationBuilder builder)
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
    }
}
