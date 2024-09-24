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

		public virtual object Call(IUnityContainer container)
		{
			var serviceProvider = unity.Resolve<IServiceProvider>();
			return implementationFactory(serviceProvider);
		}
	}

	internal class SingleTOnCallFactoryHelper : CallFactoryHelper
	{
		public SingleTOnCallFactoryHelper(IUnityContainer unity, Func<IServiceProvider, object> implementationFactory) : base(unity, implementationFactory)
		{
		}

		bool wasCalled = false;
		object resutl;

		public override object Call(IUnityContainer container)
		{
			if (!wasCalled)
				resutl = base.Call(container);
			wasCalled = true;
			return resutl;

		}
	}
}