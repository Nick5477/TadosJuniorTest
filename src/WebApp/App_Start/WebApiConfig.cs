using System.Web.Http;
using System.Web.Http.Routing.Constraints;
using Autofac;
using Autofac.Integration.WebApi;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Db.Client.Commands;
using Infrastructure.Db.Commands;
using Infrastructure.Db.Queries;
using Infrastructure.Db.Stats.Queries;
using Infrastructure.TypedFactory;
using WebApp.Controllers;
using Microsoft.ApplicationInsights.Extensibility;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterType<ClientService>()
                .As<IClientService>();
            containerBuilder
                .RegisterType<BillService>()
                .As<IBillService>();
            containerBuilder
                .RegisterType<StatsService>()
                .As<IStatsService>();

            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .SingleInstance();

            containerBuilder
                .RegisterAssemblyTypes(typeof(AddClientCommand).Assembly)
                .AsClosedTypesOf(typeof(ICommand<>));
            containerBuilder
                .RegisterAssemblyTypes(typeof(GetAllBillsStatQuery).Assembly)
                .AsClosedTypesOf(typeof(IQuery<,>));

            containerBuilder.RegisterTypedFactory<ICommandFactory>();
            containerBuilder.RegisterTypedFactory<IQueryFactory>();
            containerBuilder.RegisterTypedFactory<IQueryBuilder>();

            containerBuilder
                .RegisterType<CommandBuilder>()
                .As<ICommandBuilder>();
            containerBuilder
                .RegisterGeneric(typeof(QueryFor<>))
                .As(typeof(IQueryFor<>));

            containerBuilder.RegisterType<ClientController>().PropertiesAutowired();
            containerBuilder.RegisterType<BillController>().PropertiesAutowired();
            containerBuilder.RegisterType<StatsController>().PropertiesAutowired();

            containerBuilder.RegisterType<App>();
            IContainer container = containerBuilder.Build();
            config.DependencyResolver=new AutofacWebApiDependencyResolver(container);

            App app = container.Resolve<App>();

            app.StartInitialization();
            // Маршруты Web API
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "GetClient",
                routeTemplate: "client/{id}",
                defaults: new { controller = "Client", id = RouteParameter.Optional },
                constraints: new
                {
                    id = new IntRouteConstraint()
                }
                );

            config.Routes.MapHttpRoute(
                name: "Controllers",
                routeTemplate:"{controller}s",
                defaults: new { action = "List" }
                );

            config.Routes.MapHttpRoute(
                name: "SimpleRoute",
                routeTemplate: "{controller}/{action}"
                );

            config.Routes.MapHttpRoute(
                name: "DefaultRoute",
                routeTemplate: "{controller=Home}/{action=Index}"
                );

            config.Routes.MapHttpRoute(
                name: "ControllerIdAction",
                routeTemplate: "{controller}/{id}/{action}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new
                {
                    action = new AlphaRouteConstraint()
                }
                );

            // отключаем возможность вывода данных в формате xml
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            TelemetryConfiguration.Active.DisableTelemetry = true;
        }
    }
}
