using Ardalis.ListStartupServices;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace FundAdministration.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseAndMapMiddleWares(this WebApplication app)
        {
            app.UseExceptionHandler();
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
                });
                app.UseDeveloperExceptionPage();
                app.UseShowAllServicesMiddleware();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.MapControllers();

            app.MapHealthChecks("/healthChecks", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
