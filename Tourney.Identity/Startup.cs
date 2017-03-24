using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tourney.Identity
{
    public class Startup
    {
        private static class Secrets
        {
            public const string AzureAdClientId = "AzureAd:ClientId";
            public const string AzureAdClientSecret = "AzureAd:ClientSecret";
            public const string AzureAdAuthority = "AzureAd:Authority";
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Identities.GetIdentityResources())
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryApiResources(Resources.Get())
                .AddTestUsers(TestUsers.Users);

            //For azure ad
            services.AddAuthentication(sharedOptions =>
                sharedOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            //See AzureAdOpenConnectionMiddlewareExtension
            app.UseAzureAdOpenConnect(new AzureAdSettings()
            {
                Authority = Configuration[Secrets.AzureAdAuthority],
                ClientId = Configuration[Secrets.AzureAdClientId],
                ClientSecret = Configuration[Secrets.AzureAdClientSecret]
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
