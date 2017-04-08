using Autofac;
using Autofac.Features.Variance;
using MediatR;
using System.Collections.Generic;
using System.Net.Http;

namespace Tourney.Services.Tournaments.Client
{
    public class TournamentClientModule : Module
    {
        public string BaseUri { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri(BaseUri)
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            builder.RegisterInstance(httpClient);

            // enables contravariant Resolve() for interfaces with single contravariant ("in") arg
            builder
              .RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            // request handlers
            builder
              .Register<SingleInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => { object o; return c.TryResolve(t, out o) ? o : null; };
              })
              .InstancePerLifetimeScope();

            // notification handlers
            builder
              .Register<MultiInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
              })
              .InstancePerLifetimeScope();

            // finally register our custom code (individually, or via assembly scanning)
            // - requests & handlers as transient, i.e. InstancePerDependency()
            // - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
            // - behaviors as transient, i.e. InstancePerDependency()
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces(); // via assembly scan
            //builder.RegisterType<MyHandler>().AsImplementedInterfaces().InstancePerDependency();   
        }
    }
}
