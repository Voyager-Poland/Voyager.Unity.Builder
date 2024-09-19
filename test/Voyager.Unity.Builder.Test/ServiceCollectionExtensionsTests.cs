using Microsoft.Extensions.DependencyInjection;
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
			Type registeredType = typeof(IServiceA);
			Type mappedToType = typeof(ServiceA);
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
			Type registeredType = typeof(IServiceA);
			Type mappedToType = typeof(ServiceA);
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
			Type mappedToType = typeof(ServiceA);
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
			Type registeredType = typeof(IServiceA);
			ServiceLifetime lifetime = ServiceLifetime.Transient;

			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(() =>
					services.RegisterType(registeredType, null, lifetime));
			Assert.AreEqual("mappedToType", ex.ParamName);
		}
	}



	// Przykładowe klasy
	public interface IServiceA { }

	public class ServiceA : IServiceA { }
}