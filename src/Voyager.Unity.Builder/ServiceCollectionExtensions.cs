using Microsoft.Extensions.DependencyInjection;
using System;

namespace Voyager.Unity.Builder
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterType(
			this IServiceCollection services,
			Type registeredType,
			Type mappedToType,
			ServiceLifetime lifetime)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (registeredType == null) throw new ArgumentNullException(nameof(registeredType));
			if (mappedToType == null) throw new ArgumentNullException(nameof(mappedToType));

			// Obsługa fabryki, jeśli została dostarczona
			{
				// Rejestracja typu bez fabryki
				switch (lifetime)
				{
					case ServiceLifetime.Singleton:
						services.AddSingleton(registeredType, mappedToType);
						break;
					case ServiceLifetime.Scoped:
						services.AddScoped(registeredType, mappedToType);
						break;
					case ServiceLifetime.Transient:
						services.AddTransient(registeredType, mappedToType);
						break;
				}
			}

			return services;
		}

		public static IServiceCollection RegisterInstance(
				this IServiceCollection services,
				Type type,
				object instance,
				ServiceLifetime lifetime = ServiceLifetime.Singleton)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (type == null) throw new ArgumentNullException(nameof(type));
			if (instance == null) throw new ArgumentNullException(nameof(instance));

			// Rejestracja instancji na podstawie cyklu życia
			switch (lifetime)
			{
				case ServiceLifetime.Singleton:
					services.AddSingleton(type, instance);
					break;
				case ServiceLifetime.Scoped:
					services.AddScoped(_ => instance);
					break;
				case ServiceLifetime.Transient:
					services.AddTransient(_ => instance);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
			}

			return services;
		}

		public static IServiceCollection RegisterType<TFrom, TTo>(this IServiceCollection container) where TTo : TFrom
		{
			container.RegisterType(typeof(TFrom), typeof(TTo), ServiceLifetime.Transient);
			return container;
		}
	}
}
