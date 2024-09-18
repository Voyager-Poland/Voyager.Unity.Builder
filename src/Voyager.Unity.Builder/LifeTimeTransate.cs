using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Unity.Lifetime;

namespace Voyager.Unity.Builder
{
	class LifeTimeTransate : LifetimeManager, IFactoryLifetimeManager, ITypeLifetimeManager, IInstanceLifetimeManager
	{
		private readonly ServiceLifetime lifetime;

		public LifeTimeTransate(ServiceLifetime lifetime)
		{
			this.lifetime = lifetime;
		}


		protected override LifetimeManager OnCreateLifetimeManager()
		{
			return this.LocCreateLifetimePolicy();
		}


		LifetimeManager LocCreateLifetimePolicy()
		{
			if (lifetime == ServiceLifetime.Singleton)
				return new ContainerControlledLifetimeManager();
			else if (lifetime == ServiceLifetime.Scoped)
				return new HierarchicalLifetimeManager();
			return new TransientLifetimeManager();
		}
	}

	class SingletonLifetimeManager : LifetimeManager
	{
		private static Dictionary<Guid, object> values;
		private readonly Guid key;

		static SingletonLifetimeManager()
		{
			EnsureValues();
		}
		public SingletonLifetimeManager() : this(Guid.NewGuid())
		{
		}

		public SingletonLifetimeManager(Guid guid)
		{
			key = guid;
		}
		public override void SetValue(object newValue, ILifetimeContainer container = null)
		{
			values[key] = newValue;
		}
		public override object GetValue(ILifetimeContainer container = null)
		{
			values.TryGetValue(key, out var value);
			return value;
		}


		public override void RemoveValue(ILifetimeContainer container = null)
		{

		}

		private static void EnsureValues()
		{
			if (values == null)
			{
				values = new Dictionary<Guid, object>();
			}
		}

		protected override LifetimeManager OnCreateLifetimeManager()
		{
			return new SingletonLifetimeManager();
		}
	}
}
