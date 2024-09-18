using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace Voyager.Unity.Builder
{
	public class ScopeFactory : Microsoft.Extensions.DependencyInjection.IServiceScopeFactory
	{
		private IUnityContainer unc;

		public ScopeFactory(IUnityContainer unc)
		{
			this.unc = unc;
		}

		public IServiceScope CreateScope()
		{
			return new Scope(unc);
		}

		class Scope : IServiceScope
		{
			private IUnityContainer scopedUnc;

			public Scope(IUnityContainer unc)
			{
				this.scopedUnc = unc.CreateChildContainer();
				this.ServiceProvider = scopedUnc.Resolve<IServiceProvider>();
			}

			public IServiceProvider ServiceProvider { get; private set; }

			public void Dispose()
			{
				scopedUnc.Dispose();
			}
		}
	}
}
