using APP.Data.Repositories;
using APP.Domain.Contracts.Repositories;
using Autofac;

namespace APP.IOC
{
    /// <summary>
    /// Classe responsável por injetar repositories  na aplicação usando autofac
    /// </summary>
    public static class InjectorRepositories
    {
        /// <summary>
        /// Configura os repositories
        /// </summary>
        /// <param name="builder">Container do autofac</param>
        public static void ConfigureRepositories(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>));

            builder.RegisterAssemblyTypes(typeof(RepositoryBase<>).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        }
    }
}
