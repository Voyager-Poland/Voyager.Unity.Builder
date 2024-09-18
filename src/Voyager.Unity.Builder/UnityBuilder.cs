using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;
using Unity.Lifetime;

namespace Voyager.Unity.Builder
{
	public class UnityBuilder : Microsoft.Extensions.DependencyInjection.IServiceProviderFactory<IUnityContainer>
	{
		readonly IUnityContainer unity;

		public UnityBuilder(IUnityContainer container = null)
		{
			unity = container ?? new UnityContainer();
		}


		public IUnityContainer CreateBuilder(IServiceCollection services)
		{
			RegisterServiceProvider();

			if (services != null)
			{
				foreach (var description in services)
				{
					if (description.ImplementationFactory != null)
					{
						IFactoryLifetimeManager factoryLifetimeManager = new LifeTimeTransate(description.Lifetime);
						CallFactoryHelper myCallFactory = new CallFactoryHelper(unity, this, description.ImplementationFactory);
						unity.RegisterFactory(description.ServiceType, myCallFactory.Call, factoryLifetimeManager);
					}
					else if (description.ImplementationInstance != null)
					{
						unity.RegisterInstance(description.ServiceType, description.ImplementationInstance);
					}
					else
					{
						ITypeLifetimeManager lifeLiem = new LifeTimeTransate(description.Lifetime);
						unity.RegisterType(description.ServiceType, description.ImplementationType, GetLifetimeManager(description.Lifetime));
					}
				}

			}
			return unity;
		}

		private void RegisterServiceProvider()
		{
			unity.RegisterType<IServiceProvider, VoyServiceProvider>();
			unity.RegisterFactory<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>(unc =>
			{
				return new ScopeFactory(unc);
			});
		}

		public void AddDiagnostic()
		{
			unity.AddExtension(new Diagnostic());
		}
		private static ITypeLifetimeManager GetLifetimeManager(ServiceLifetime lifetime)
		{
			switch (lifetime)
			{
				case ServiceLifetime.Singleton:
					return new ContainerControlledLifetimeManager(); // Singleton
				case ServiceLifetime.Scoped:
					return new HierarchicalLifetimeManager(); // Scoped
				case ServiceLifetime.Transient:
					return new TransientLifetimeManager(); // Transient
				default:
					throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
			}
		}

		T CallServiceFactory<T>(IUnityContainer unityContainer, Func<IServiceProvider, T> funcjaDoWywolania)
		{
			var serviceProvider = this.CreateServiceProvider(unityContainer);
			return funcjaDoWywolania(serviceProvider);
		}

		public IServiceProvider CreateServiceProvider(IUnityContainer containerBuilder)
		{
			return containerBuilder.Resolve<IServiceProvider>();
		}


	}
}
