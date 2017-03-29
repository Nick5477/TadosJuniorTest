using System.Collections.Generic;
using Infrastructure.Db.Commands;
using Autofac;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Domain.Structures;
using Infrastructure.TypedFactory;
using Infrastructure.Db.Client.Commands;
using Infrastructure.Db.Queries;
using Infrastructure.Db.Stats.Queries;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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

            containerBuilder.RegisterType<App>();
            IContainer container = containerBuilder.Build();
            App app = container.Resolve<App>();
            app.Start1();
            //app.AddNewClient("dsd", "0000000000");
            //app.AddNewClient("dddddd", "0000000001");
            //app.AddNewBill(1, 100);
            //app.AddNewBill(2, 1111);
            //app.PayBill(1);
            IEnumerable<Bill> bills = app.GetBills(0, 100);
            IEnumerable<Bill> clientBills = app.GetClientBills(2,0, 100);
            IEnumerable<Client> clients = app.GetClients(0, 100);
            BillsStat billsStat = app.GetAllBillsStat();
            IEnumerable<ClientPayedBillsSum> clientPayedBillsSums = 
                app.GetPayedBillsSums(2, 
                "",
                "");
            BillsStat clientStat=app.GetClientBillsStat(1, "", "");

            //app.DeleteClient(1);
            container.Dispose();
        }
    }
}
