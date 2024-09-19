using Microsoft.Extensions.DependencyInjection;

namespace Voyager.Unity.Builder.Test
{
	[TestFixture]
	class ServiceCollectionExtensionsInstanceTests
	{
		private IServiceCollection services;

		[SetUp]
		public void SetUp()
		{
			services = new ServiceCollection(); // Tworzymy nową ServiceCollection przed każdym testem
		}

		[Test]
		public void RegisterInstance_AddsSingletonServiceDescriptor_WhenLifetimeIsSingleton()
		{
			// Arrange
			var instance = new MyService();
			Type registeredType = typeof(IMyService);

			// Act
			services.RegisterInstance(registeredType, instance, ServiceLifetime.Singleton);

			// Assert
			var serviceDescriptor = services.GetServiceDescriptorForType(registeredType);
			Assert.NotNull(serviceDescriptor);
			Assert.AreEqual(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);
			Assert.AreSame(instance, serviceDescriptor.ImplementationInstance);
		}

		[Test]
		public void RegisterInstance_AddsScopedServiceDescriptor_WhenLifetimeIsScoped()
		{
			// Arrange
			var instance = new MyService();
			Type registeredType = typeof(IMyService);

			// Act
			services.RegisterInstance(registeredType, instance, ServiceLifetime.Scoped);

			// Assert
			var serviceDescriptor = services.GetServiceDescriptorForType(registeredType);
			Assert.NotNull(serviceDescriptor);
			Assert.AreEqual(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
			Assert.AreSame(instance, serviceDescriptor.ImplementationInstance);
		}

		[Test]
		public void RegisterInstance_AddsTransientServiceDescriptor_WhenLifetimeIsTransient()
		{
			// Arrange
			var instance = new MyService();
			Type registeredType = typeof(IMyService);

			// Act
			services.RegisterInstance(registeredType, instance, ServiceLifetime.Transient);

			// Assert
			var serviceDescriptor = services.GetServiceDescriptorForType(registeredType);
			Assert.NotNull(serviceDescriptor);
			Assert.AreEqual(ServiceLifetime.Transient, serviceDescriptor.Lifetime);
			Assert.AreSame(instance, serviceDescriptor.ImplementationInstance);
		}

		[Test]
		public void RegisterInstance_ThrowsArgumentNullException_WhenRegisteredTypeIsNull()
		{
			// Arrange
			var instance = new MyService();

			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(() =>
					services.RegisterInstance(null, instance, ServiceLifetime.Singleton));
			Assert.AreEqual("type", ex.ParamName);
		}

		[Test]
		public void RegisterInstance_ThrowsArgumentNullException_WhenInstanceIsNull()
		{
			// Arrange
			Type registeredType = typeof(IMyService);

			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(() =>
					services.RegisterInstance(registeredType, null, ServiceLifetime.Singleton));
			Assert.AreEqual("instance", ex.ParamName);
		}
	}

	// Pomocnicza metoda rozszerzająca do pobierania ServiceDescriptor

	// Przykładowe interfejsy i klasy
	public interface IMyService { }

	public class MyService : IMyService { }

}

