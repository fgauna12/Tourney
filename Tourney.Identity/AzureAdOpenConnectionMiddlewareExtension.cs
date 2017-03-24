using Microsoft.AspNetCore.Builder;

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
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "Azure AD",
                DisplayName = "Azure AD", 
                ClientId = settings.ClientId,
                //ClientSecret = ClientSecret, // for code flow
                Authority = settings.Authority,
                //ResponseType = OpenIdConnectResponseType.CodeIdToken,
                PostLogoutRedirectUri = "/account/signout",
                // GetClaimsFromUserInfoEndpoint = true,
            });
            return app;
        }
    }                //Events = new OpenIdConnectEvents()
                //{
                //    OnAuthorizationCodeReceived = async context =>
                //    {
                //        var request = context.HttpContext.Request;
                //        var currentUri = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path);
                //        var credential = new ClientCredential(clientId, clientSecret);
                //        var authContext = new AuthenticationContext(authority, AuthPropertiesTokenCache.ForCodeRedemption(context.Properties));

                //        var result = await authContext.AcquireTokenByAuthorizationCodeAsync(
                //            context.ProtocolMessage.Code, new Uri(currentUri), credential, resource);

                //        context.HandleCodeRedemption(result.AccessToken, result.IdToken);
                //    }
                //}
}
