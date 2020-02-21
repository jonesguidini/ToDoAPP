using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace APP.CrossCutting
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Aplica todas as configuracoes de mapeamento de um Assembly
        /// </summary>
        /// <param name="modelBuilder">Model Builder</param>
        /// <param name="assembly">Assembly</param>
        /// <param name="configNamespace">namespace onde estao os mapeamentos de banco</param>
        public static void ApplyAllConfigurationsFromCurrentAssembly(this ModelBuilder modelBuilder, Assembly assembly, string configNamespace = "")
        {
            var applyGenericMethods = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m => m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));
            var applyGenericMethod = applyGenericApplyConfigurationMethods.FirstOrDefault(m => m.GetParameters().FirstOrDefault()?.ParameterType.Name == "IEntityTypeConfiguration`1");

            var applicableTypes = assembly
                .GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters);

            if (!string.IsNullOrEmpty(configNamespace))
            {
                applicableTypes = applicableTypes.Where(c => c.Namespace == configNamespace);
            }

            foreach (var type in applicableTypes)
            {
                foreach (var iface in type.GetInterfaces()
                    .Where(i => i.IsConstructedGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                {
                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);

                    applyConcreteMethod.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
                    break;
                }
            }
        }
    }
}
