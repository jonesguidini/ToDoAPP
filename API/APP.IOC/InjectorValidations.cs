using APP.Domain.Contracts.FluentValidation;
using APP.Domain.Entities.FluentValidation;
using Autofac;

namespace APP.IOC
{

    /// <summary>
    /// Classe responsável por injeta Validations na aplicação usando autofac
    /// </summary>
    public static class InjectorValidations
    {
        /// <summary>
        /// Configura a injeção das Validations
        /// </summary>
        /// <param name="builder">Container do autofac</param>
        public static void ConfigureValidations(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(FluentValidation<>)).As(typeof(IFluentValidation<>));

            builder.RegisterAssemblyTypes(typeof(FluentValidation<>).Assembly)
            .Where(t => t.Name.EndsWith("Validation"))
            .InstancePerLifetimeScope();
        }
    }
}
