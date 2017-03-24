using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Db.Client.Commands;
using Infrastructure.Db.Commands;
using Infrastructure.Db.Queries;
using Infrastructure.Db.Stat.Queries;
using Infrastructure.TypedFactory;
using WebApp.Controllers;

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
                .RegisterType<StatService>()
                .As<IStatService>();

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
            containerBuilder.RegisterType<App>();
            IContainer container = containerBuilder.Build();
            config.DependencyResolver=new AutofacWebApiDependencyResolver(container);

            App app = container.Resolve<App>();

            app.StartInitialization();
            // Маршруты Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Controllers",
                routeTemplate:"{controller}s",
                defaults: new { action = "List" }
                );
            config.Routes.MapHttpRoute(
                name: "GetClient",
                routeTemplate:"Client/{id}",
                defaults:new {controller="Client",action="Get"}
                );
        }
    }
}
