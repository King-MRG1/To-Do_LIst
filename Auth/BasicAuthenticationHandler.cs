using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using To_Do_List.Models;

namespace To_Do_List.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ListContext _context;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder,ListContext context) : base(options, logger, encoder)
        {
            _context = context;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
           if(!Request.Headers.ContainsKey("Authorization"))
                return System.Threading.Tasks.Task.FromResult(AuthenticateResult.Fail("Missing Authorization Key"));

           var authorizationHeader = Request.Headers["Authorization"].ToString();

            if(!authorizationHeader.StartsWith("Basic",StringComparison.OrdinalIgnoreCase))
                return System.Threading.Tasks.Task.FromResult(AuthenticateResult.Fail("Invalid Authorization doesn't start with 'Basic'"));

            var authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic", "", StringComparison.OrdinalIgnoreCase)));

            var authsplit = authBase64Decoded.Split(":");

            if (authsplit.Length != 2)
                return System.Threading.Tasks.Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header format"));

            var username = authsplit[0];
            var password = authsplit[1];

            var user = _context.Users.Where(u => u.Username == username && u.Password == password).SingleOrDefault();

            if (user == null)
                return System.Threading.Tasks.Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));

            var client = new BasicAuthenticationClient
            {
                Name = username,
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(client, new[] { new Claim(ClaimTypes.Name, username) }));

            return System.Threading.Tasks.Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
        }
    }
}
