using System;
using System.Diagnostics;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Tourney.Identity
{
    public class AzureAdSettings
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public static class AzureAdOpenConnectionMiddlewareExtension
    {
        public static IApplicationBuilder UseAzureAdOpenConnect(this IApplicationBuilder app, AzureAdSettings settings)
        {
            var logger = app.ApplicationServices.GetService<ILogger<Startup>>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                DisplayName = "Azure AD", 
                ClientId = settings.ClientId,
                //ClientSecret = ClientSecret, // for code flow
                Authority = settings.Authority,
                PostLogoutRedirectUri = "/account/signout",
                GetClaimsFromUserInfoEndpoint = true,
                //http://www.dushyantgill.com/blog/2014/12/10/roles-based-access-control-in-cloud-applications-using-azure-ad/
                //TokenValidationParameters = new TokenValidationParameters()
                //{
                //    // we inject our own multitenant validation logic
                //    ValidateIssuer = false,
                //    // map the claimsPrincipal's roles to the roles claim
                //    RoleClaimType = "roles",
                //},
                Events = new OpenIdConnectEvents()
                {
                    OnTicketReceived = async context =>
                    {
                        var info = JsonConvert.SerializeObject(context.Principal.Claims, new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                        await Task.FromResult(0);
                    },
                    OnUserInformationReceived = async context =>
                    {
                        logger.LogInformation(context.User.ToString());
                        await Task.FromResult(0);
                    }
                    //OnAuthorizationCodeReceived = async context =>
                    //{
                        //var request = context.HttpContext.Request;
                        //var currentUri = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path);
                        //var credential = new ClientCredential(clientId, clientSecret);
                        //var authContext = new AuthenticationContext(authority, AuthPropertiesTokenCache.ForCodeRedemption(context.Properties));

                        //var result = await authContext.AcquireTokenByAuthorizationCodeAsync(
                        //    context.ProtocolMessage.Code, new Uri(currentUri), credential, resource);

                        //context.HandleCodeRedemption(result.AccessToken, result.IdToken);
                    //}
                }
            });
            return app;
        }
    }                
}
