using System;
using Unity;

namespace Voyager.Unity.Builder
{
	internal class CallFactoryHelper
	{
		private IUnityContainer unity;
		private readonly UnityBuilder unityBuilder;
		private Func<IServiceProvider, object> implementationFactory;

		public CallFactoryHelper(IUnityContainer unity, Func<IServiceProvider, object> implementationFactory)
		{
			this.unity = unity;
			this.implementationFactory = implementationFactory;
		}

		internal object Call(IUnityContainer container)
		{
			var serviceProvider = unity.Resolve<IServiceProvider>();
			return implementationFactory(serviceProvider);
		}
	}
}