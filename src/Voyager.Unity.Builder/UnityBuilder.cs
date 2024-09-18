using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace Voyager.Unity.Builder
{
	public class UnityBuilder : Microsoft.Extensions.DependencyInjection.IServiceProviderFactory<IUnityContainer>
	{
		readonly IUnityContainer unity;

		public UnityBuilder(IUnityContainer container = null)
		{
			unity = container ?? new UnityContainer();
		}
		public IUnityContainer CreateBuilder(IServiceCollection services)
		{
			RegisterServiceProvider();

			if (services != null)
			{
				foreach (var description in services)
				{
					Register(description);
				}

			}
			return unity;
		}

		private void Register(ServiceDescriptor description)
		{
			var registerProces = new RegisterProces(description, unity);
			registerProces.Register();

		}

		public IServiceProvider CreateServiceProvider(IUnityContainer containerBuilder)
		{
			return containerBuilder.Resolve<IServiceProvider>();
		}

		private void RegisterServiceProvider()
		{
			unity.RegisterType<IServiceProvider, VoyServiceProvider>();
			unity.RegisterFactory<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>(unc =>
			{
				return new ScopeFactory(unc);
			});
		}

		public void AddDiagnostic()
		{
			unity.AddExtension(new Diagnostic());
		}
	}
}
