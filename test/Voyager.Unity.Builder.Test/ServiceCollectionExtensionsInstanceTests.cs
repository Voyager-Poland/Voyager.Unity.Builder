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
			var instance = new ExampleClass();
			Type registeredType = typeof(IExampleInterface);

			// Act
			services.RegisterInstance(registeredType, instance);

			// Assert
			var serviceDescriptor = services.GetServiceDescriptorForType(registeredType);
			Assert.NotNull(serviceDescriptor);
			Assert.AreEqual(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);
			Assert.AreSame(instance, serviceDescriptor.ImplementationInstance);
		}

		[Test]
		public void RegisterInstance_ThrowsArgumentNullException_WhenRegisteredTypeIsNull()
		{
			// Arrange
			var instance = new ExampleClass();

			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(() =>
					services.RegisterInstance(null, instance));
			Assert.AreEqual("type", ex.ParamName);
		}

		[Test]
		public void RegisterInstance_ThrowsArgumentNullException_WhenInstanceIsNull()
		{
			// Arrange
			Type registeredType = typeof(IExampleInterface);

			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(() =>
					services.RegisterInstance(registeredType, null));
			Assert.AreEqual("instance", ex.ParamName);
		}
	}
}

