using Microsoft.Extensions.DependencyInjection;
using Unity.Lifetime;

namespace Voyager.Unity.Builder
{
	class LifeTimeTransate : LifetimeManager, IFactoryLifetimeManager, ITypeLifetimeManager, IInstanceLifetimeManager
	{
		private ServiceLifetime lifetime;

		public LifeTimeTransate(ServiceLifetime lifetime)
		{
			this.lifetime = lifetime;
		}

		public LifetimeManager CreateLifetimePolicy()
		{
			if (lifetime == ServiceLifetime.Singleton)
				return new SingletonLifetimeManager();
			else if (lifetime == ServiceLifetime.Scoped)
				return new PerThreadLifetimeManager();
			return new PerResolveLifetimeManager();
		}

		protected override LifetimeManager OnCreateLifetimeManager()
		{
			return this.CreateLifetimePolicy();
		}
	}
}
