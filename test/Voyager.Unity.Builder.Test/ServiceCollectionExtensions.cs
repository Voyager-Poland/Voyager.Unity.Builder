using Microsoft.Extensions.DependencyInjection;
namespace Voyager.Unity.Builder.Test
{
	public static class ServiceCollectionExtensions
	{
		// Metoda do znajdywania ServiceDescriptor w ServiceCollection na potrzeby testów
		public static ServiceDescriptor GetServiceDescriptorForType(this IServiceCollection services, Type serviceType)
		{
			foreach (var descriptor in services)
			{
				if (descriptor.ServiceType == serviceType)
				{
					return descriptor;
				}
			}

			return null;
		}
	}
}