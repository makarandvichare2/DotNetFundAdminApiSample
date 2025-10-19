using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace FundAdministration.API
{
    public class MockAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public MockAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                               ILoggerFactory logger,
                               UrlEncoder encoder,
                               ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "MockUser"),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin")
        };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "Mock");
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Mock");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
