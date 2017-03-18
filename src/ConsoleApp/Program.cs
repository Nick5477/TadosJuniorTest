using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Db;
using Infrastructure.Db.Commands;
using Autofac;
using Domain.Entities;
using Domain.Services;
using Infrastructure.TypedFactory;
using Infrastructure.Db.Client.Commands;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var containerBuilder = new ContainerBuilder();
            //containerBuilder.RegisterTypedFactory<ICommandFactory>();
            //containerBuilder
            //    .RegisterType<CommandBuilder>()
            //    .As<ICommandBuilder>();
            //containerBuilder
            //    .RegisterAssemblyTypes(typeof(AddClientCommand).Assembly)
            //    .AsClosedTypesOf(typeof(ICommand<>));
            //containerBuilder.RegisterType<App>();
            //IContainer container = containerBuilder.Build();
            //App app = container.Resolve<App>();
            //app.AddNewClient(1, "dsd", "asdad");
            //app.ChangeClientName(1, "111");
            //app.AddNewBill(1, 1, 1000, 1, DateTime.UtcNow);
            //app.PayBill(1);
            //app.DeleteClient(1);
            //container.Dispose();
            List<string> str=new List<string>();
            str.Add("11.2005-000009");
            str.Add("11.2005-000100");
            str.Add("12.2006-000001");
            Console.WriteLine("12345".CompareTo(""));
        }
    }
}
