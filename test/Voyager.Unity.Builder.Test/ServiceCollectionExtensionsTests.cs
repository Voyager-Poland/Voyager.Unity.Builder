using Microsoft.Extensions.DependencyInjection;
using Unity;
namespace Voyager.Unity.Builder.Test
{
	[TestFixture]
	public class ServiceCollectionExtensionsTests
	{
		private IServiceCollection services;

		[SetUp]
		public void SetUp()
		{
			services = new ServiceCollection(); // Tworzymy nowy ServiceCollection przed każdym testem
		}

		[Test]
		public void RegisterType_RegistersTypeAsTransient_WhenLifetimeIsTransient()
		{
			// Arrange
			Type registeredType = typeof(IExampleInterface);
			Type mappedToType = typeof(ExampleClass);
			ServiceLifetime lifetime = ServiceLifetime.Transient;

			// Act
			services.RegisterType(registeredType, mappedToType, lifetime);

			// Assert
			var serviceDescriptor = services.GetServiceDescriptorForType(registeredType);
			Assert.NotNull(serviceDescriptor);
			Assert.AreEqual(ServiceLifetime.Transient, serviceDescriptor.Lifetime);
			Assert.AreEqual(mappedToType, serviceDescriptor.ImplementationType);
		}

		[Test]
		public void RegisterType_RegistersTypeAsSingleton_WhenLifetimeIsSingleton()
		{
			// Arrange
			Type registeredType = typeof(IExampleInterface);
			Type mappedToType = typeof(ExampleClass);
			ServiceLifetime lifetime = ServiceLifetime.Singleton;

			// Act
			services.RegisterType(registeredType, mappedToType, lifetime);

			// Assert
			var serviceDescriptor = services.GetServiceDescriptorForType(registeredType);
			Assert.NotNull(serviceDescriptor);
			Assert.AreEqual(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);
			Assert.AreEqual(mappedToType, serviceDescriptor.ImplementationType);
		}

		[Test]
		public void RegisterType_ThrowsArgumentNullException_WhenRegisteredTypeIsNull()
		{
			// Arrange
			Type mappedToType = typeof(ExampleClass);
			ServiceLifetime lifetime = ServiceLifetime.Transient;

			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(() =>
					services.RegisterType(null, mappedToType, lifetime));
			Assert.AreEqual("registeredType", ex.ParamName);
		}

		[Test]
		public void RegisterType_ThrowsArgumentNullException_WhenMappedToTypeIsNull()
		{
			// Arrange
			Type registeredType = typeof(IExampleInterface);
			ServiceLifetime lifetime = ServiceLifetime.Transient;

			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(() =>
					services.RegisterType(registeredType, null, lifetime));
			Assert.AreEqual("mappedToType", ex.ParamName);
		}
	}

	[TestFixture]
	public class ServiceCollectionExtensionsBuidTests
	{
		private IServiceCollection services;
		private UnityBuilder builder;

		[SetUp]
		public void SetUp()
		{
			services = new ServiceCollection();
			builder = new UnityBuilder();
		}

		[Test]
		public void RegisterType_TFrom_TTo_RegistersTypesInUnityContainer()
		{
			// Arrange
			services.RegisterType<IExampleInterface, ExampleClass>();
			var container = builder.CreateBuilder(services);

			// Act
			var resolvedInstance = container.Resolve<IExampleInterface>();

			// Assert
			Assert.IsInstanceOf<ExampleClass>(resolvedInstance);
		}

		[Test]
		public void RegisterType_TTo_RegistersSelfInUnityContainer()
		{
			// Arrange
			services.RegisterType<ExampleClass>();
			var container = builder.CreateBuilder(services);

			// Act
			var resolvedInstance = container.Resolve<ExampleClass>();

			// Assert
			Assert.IsInstanceOf<ExampleClass>(resolvedInstance);
		}
	}
}