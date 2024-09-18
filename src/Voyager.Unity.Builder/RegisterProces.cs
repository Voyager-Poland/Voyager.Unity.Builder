using Microsoft.Extensions.DependencyInjection;
using Unity;
using Unity.Lifetime;

namespace Voyager.Unity.Builder
{
	class RegisterProces
	{
		private ServiceDescriptor description;
		private IUnityContainer unity;

		public RegisterProces(ServiceDescriptor description, IUnityContainer unity)
		{
			this.description = description;
			this.unity = unity;
		}

		internal void Register()
		{
			if (description.ImplementationFactory != null)
			{
				IFactoryLifetimeManager factoryLifetimeManager = new LifeTimeTransate(description.Lifetime);
				CallFactoryHelper myCallFactory = new CallFactoryHelper(unity, description.ImplementationFactory);
				unity.RegisterFactory(description.ServiceType, myCallFactory.Call, factoryLifetimeManager);
			}
			else if (description.ImplementationType != null)
				unity.RegisterType(description.ServiceType, description.ImplementationType, GetLifetimeManager(description.Lifetime));
			else
				unity.RegisterInstance(description.ServiceType, description.ImplementationInstance);
		}

		private static ITypeLifetimeManager GetLifetimeManager(ServiceLifetime lifetime)
		{
			switch (lifetime)
			{
				case ServiceLifetime.Singleton:
					return new ContainerControlledLifetimeManager();
				case ServiceLifetime.Scoped:
					return new HierarchicalLifetimeManager();
				default:
					return new TransientLifetimeManager();
			}
		}
	}
}
