using System;
using Unity;

namespace Voyager.Unity.Builder
{
	class MyServiceProvider : IServiceProvider
	{
		private IUnityContainer containerBuilder;

		public MyServiceProvider(IUnityContainer containerBuilder)
		{
			this.containerBuilder = containerBuilder;
		}

		public object GetService(Type serviceType)
		{
			return containerBuilder.Resolve(serviceType);
		}
	}
}
