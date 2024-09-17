using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace Voyager.Unity.Builder
{
	public partial class UnityBuilder : Microsoft.Extensions.DependencyInjection.IServiceProviderFactory<IUnityContainer>
	{
		public IUnityContainer CreateBuilder(IServiceCollection services)
		{
			UnityContainer unity = new UnityContainer();
			return unity;
		}

		public IServiceProvider CreateServiceProvider(IUnityContainer containerBuilder)
		{
			return new MyServiceProvider(containerBuilder);
		}
	}
}
