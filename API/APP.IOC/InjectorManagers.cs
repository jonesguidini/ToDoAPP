using Autofac;
using APP.Business.Managers;

namespace APP.IOC
{
    /// <summary>
    /// Classe responsável por injetar Managers na aplicação usando autofac
    /// </summary>
    public static class InjectorManagers
    {
        /// <summary>
        /// Configura a injeção dos Serviços
        /// </summary>
        /// <param name="builder">Container do autofac</param>
        public static void ConfigureManagers(this ContainerBuilder builder)
        {
            builder.RegisterType<NotificationManager>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
