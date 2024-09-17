
using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace Voyager.Unity.Builder.Test
{
	class TestResult
	{
		private readonly IUnityContainer unity;
		private readonly IServiceProvider provider;

		public TestResult(UnityBuilder unityBuilder, IServiceCollection serviceDescriptors)
		{

			this.unity = unityBuilder.CreateBuilder(serviceDescriptors);
			this.provider = unityBuilder.CreateServiceProvider(unity);
		}

		public IUnityContainer Unity => unity;
		public IServiceProvider ServiceProvicer => provider;
	}
}