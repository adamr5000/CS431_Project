using System;
using System.IO;
using System.Net.Sockets;
using Autofac;
using Microsoft.Owin.Hosting;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Owin;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.PostgreSQL;

namespace CS431_Project
{
    // If you're having trouble getting a class you just created to get resolved, you're in the right place (otherwise, probably not)
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider => new SelfHostRootPathProvider();

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
        }

        bool PortOpen(int port)
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect("127.0.0.1", port);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static string NoDBConnectionString = "";

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            // Perform registration that should have an application lifetime
            var builder = new ContainerBuilder();

            if (PortOpen(3306))
            {
                // Log "Using MySQL"
                OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
                builder.RegisterInstance(
                    new OrmLiteConnectionFactory(
                        "Server=localhost;Port=3306;User Id=root;Password=password;Database=cs431project;"));
                NoDBConnectionString = "Server=localhost;Port=3306;User Id=root;Password=password;";
            }
            else if(PortOpen(5432))
            {
                // Log "Using Postgres"
                OrmLiteConfig.DialectProvider = PostgreSqlDialect.Provider;
                OrmLiteConfig.DialectProvider.NamingStrategy = new PostgreSqlNamingStrategy();
                builder.RegisterInstance(
                    new OrmLiteConnectionFactory(
                        @"Server=localhost;Port=5432;Database=cs431project;User Id=postgres;Password=password;Database=cs431project;"));
                NoDBConnectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=password;";
            }
            else throw new Exception("No local database detected");

            builder.Update(existingContainer.ComponentRegistry);
        }

        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime

            base.ConfigureRequestContainer(container, context);

            var builder = new ContainerBuilder();
            //builder.RegisterType<Foo>().As<IFoo>().SingleInstance();
            builder.Update(container.ComponentRegistry);
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
        }
        public T Resolve<T>()
        {
            return this.ApplicationContainer.Resolve<T>();
        }
    }

    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // If this throws an error, see "Running without Admin mode" mentioned here:
            // https://github.com/NancyFx/Nancy/wiki/Hosting-nancy-with-owin#katana---httplistener-selfhost
            app.UseNancy();
        }
    }

    public class SelfHost
    {
        static void Main(string[] args)
        {
            var url = "http://+:8080";

            // If this throws an error, see "Running without Admin mode" mentioned here:
            // https://github.com/NancyFx/Nancy/wiki/Hosting-nancy-with-owin#katana---httplistener-selfhost
            using (WebApp.Start<OwinStartup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }

    // For development: get views relative to project folder rather than binary location
    // This lets us live edit the views
    // From https://github.com/NancyFx/Nancy/issues/1688
    public class SelfHostRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return StaticConfiguration.IsRunningDebug
                ? Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", ".."))
                : AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}