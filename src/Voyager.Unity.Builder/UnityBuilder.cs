﻿using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;
using Unity.Lifetime;

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
					if (description.ImplementationFactory != null)
					{
						IFactoryLifetimeManager factoryLifetimeManager = new LifeTimeTransate(description.Lifetime);
						MyCallFactory myCallFactory = new MyCallFactory(unity, this, description.ImplementationFactory);
						unity.RegisterFactory(description.ServiceType, myCallFactory.Call, factoryLifetimeManager);
					}
					else if (description.ImplementationInstance != null)
					{
						unity.RegisterInstance(description.ServiceType, description.ImplementationInstance);
					}
					else
					{

						ITypeLifetimeManager lifeLiem = new LifeTimeTransate(description.Lifetime);
						unity.RegisterType(description.ServiceType, description.ImplementationType, null, lifeLiem);
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
