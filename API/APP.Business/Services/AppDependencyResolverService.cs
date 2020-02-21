using System;

namespace APP.Business.Services
{
    public class AppDependencyResolverService
    {
        private static AppDependencyResolverService _resolver;

        public static AppDependencyResolverService Current
        {
            get
            {
                if (_resolver == null)
                    throw new Exception("'AppDependencyResolver' não foi inicializado. Você deve iniciá-lo no arquivo Startup");
                return _resolver;
            }
        }

        public static void Init(IServiceProvider services)
        {
            _resolver = new AppDependencyResolverService(services);
        }

        private readonly IServiceProvider _serviceProvider;

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        private AppDependencyResolverService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

    }
}
