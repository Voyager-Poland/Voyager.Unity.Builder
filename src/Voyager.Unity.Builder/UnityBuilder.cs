using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace Voyager.Unity.Builder
{
	public class UnityBuilder : Microsoft.Extensions.DependencyInjection.IServiceProviderFactory<IUnityContainer>
	{
		public IUnityContainer CreateBuilder(IServiceCollection services)
		{
			throw new NotImplementedException();
		}

		public IServiceProvider CreateServiceProvider(IUnityContainer containerBuilder)
		{
			throw new NotImplementedException();
		}
	}
}
