using System;
using Unity;

namespace Voyager.Unity.Builder
{
	internal class CallFactoryHelper
	{
		private IUnityContainer unity;
		private readonly UnityBuilder unityBuilder;
		private Func<IServiceProvider, object> implementationFactory;

		public CallFactoryHelper(IUnityContainer unity, UnityBuilder unityBuilder, Func<IServiceProvider, object> implementationFactory)
		{
			this.unity = unity;
			this.unityBuilder = unityBuilder;
			this.implementationFactory = implementationFactory;
		}

		internal object Call(IUnityContainer container)
		{
			var serviceProvider = unityBuilder.CreateServiceProvider(unity);
			return implementationFactory(serviceProvider);
		}
	}
}