using Microsoft.Extensions.DependencyInjection;
using Unity.Lifetime;

namespace Voyager.Unity.Builder
{
	class LifeTimeTransate : LifetimeManager, IFactoryLifetimeManager
	{
		private readonly ServiceLifetime lifetime;

		public LifeTimeTransate(ServiceLifetime lifetime)
		{
			this.lifetime = lifetime;
		}

		protected override LifetimeManager OnCreateLifetimeManager() => this.LocCreateLifetimePolicy();

		LifetimeManager LocCreateLifetimePolicy()
		{
			if (lifetime == ServiceLifetime.Singleton)
				return new ContainerControlledLifetimeManager();
			else if (lifetime == ServiceLifetime.Scoped)
				return new HierarchicalLifetimeManager();
			return new TransientLifetimeManager();
		}
	}
}
