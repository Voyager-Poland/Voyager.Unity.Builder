using System;
using Unity;

namespace Voyager.Unity.Builder
{
	public class VoyServiceProvider : IServiceProvider
	{
		private IUnityContainer containerBuilder;

		public VoyServiceProvider(IUnityContainer containerBuilder)
		{
			this.containerBuilder = containerBuilder;
		}

		public object GetService(Type serviceType)
		{
			return containerBuilder.Resolve(serviceType);
		}
	}
}
