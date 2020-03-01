using APP.Business.Services;
using APP.Domain.Contracts.Services;
using Autofac;

namespace APP.IOC
{
    /// <summary>
    /// Classe responsável por injetar Services  na aplicação usando autofac
    /// </summary>
    public static class InjectorServices
    {
        /// <summary>
        /// Configura a injeção dos Serviços
        /// </summary>
        /// <param name="builder">Container do autofac</param>
        public static void ConfigureServices(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ServiceBase<>)).As(typeof(IServiceBase<>));

            builder.RegisterAssemblyTypes(typeof(ServiceBase<>).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        }
    }
}
