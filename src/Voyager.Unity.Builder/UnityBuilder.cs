using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace Voyager.Unity.Builder
{
	public class UnityBuilder : Microsoft.Extensions.DependencyInjection.IServiceProviderFactory<IUnityContainer>
	{
		public IUnityContainer CreateBuilder(IServiceCollection services)
		{
			var unity = new UnityContainer().AddExtension(new Diagnostic());
			if (services != null)
			{
				foreach (var description in services)
				{
					if (description.Lifetime == ServiceLifetime.Transient)
					{
						if (description.ImplementationFactory != null)
						{
							MyCallFactory myCallFactory = new MyCallFactory(unity, this, description.ImplementationFactory);
							unity.RegisterFactory(description.ServiceType, myCallFactory.Call);
						}
						else
							unity.RegisterType(description.ServiceType, description.ImplementationType);
					}

				}

			}
			return unity;
		}

		T CallServiceFactory<T>(IUnityContainer unityContainer, Func<IServiceProvider, T> funcjaDoWywolania)
		{
			var serviceProvider = this.CreateServiceProvider(unityContainer);
			return funcjaDoWywolania(serviceProvider);
		}

		public IServiceProvider CreateServiceProvider(IUnityContainer containerBuilder)
		{
			return new MyServiceProvider(containerBuilder);
		}


	}
}
