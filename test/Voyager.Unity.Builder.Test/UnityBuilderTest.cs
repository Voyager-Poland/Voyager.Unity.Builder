
using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace Voyager.Unity.Builder.Test
{

	public class Tests
	{
		private UnityBuilder service;
		private ServiceCollection serviceDescriptors;

		[SetUp]
		public void Setup()
		{
			service = new Voyager.Unity.Builder.UnityBuilder();
			serviceDescriptors = new ServiceCollection();
		}

		[Test]
		public void NullServicCollection()
		{
			var result = PrepareUnit(null!);
		}


		[Test]
		public void EmptyServicCollection()
		{
			this.PrepareUnit(serviceDescriptors);
		}

		[Test]
		public void TransistientType()
		{
			serviceDescriptors.AddTransient<ExampleClass>();
			var result = PrepareUnit(serviceDescriptors);
			TestType<ExampleClass>(result);
		}

		[Test]
		public void SingletonType()
		{
			serviceDescriptors.AddSingleton<ExampleClass>();
			var result = PrepareUnit(serviceDescriptors);
			TestType<ExampleClass>(result);
		}

		[Test]
		public void SingletonKeyType()
		{
			serviceDescriptors.AddSingleton<IExampleInterface, ExampleClass>();
			var result = PrepareUnit(serviceDescriptors);
			TestType<IExampleInterface>(result);
		}

		[Test]
		public void ScopeKeyType()
		{
			serviceDescriptors.AddScoped<IExampleInterface, ExampleClass>();
			var result = PrepareUnit(serviceDescriptors);
			TestType<IExampleInterface>(result);
			using (var scopeControl = result.ServiceProvicer.CreateScope())
			{
				var scopedControl = scopeControl.ServiceProvider.GetService<IExampleInterface>();
				Assert.IsNotNull(scopedControl);
				Assert.That(ExampleClass.anyDisposed, Is.False, "Test isn't well prepared");
			}
			Assert.That(ExampleClass.anyDisposed, Is.True, "Scope i");
		}

		[Test]
		public void TransistientInstance()
		{
			serviceDescriptors.AddSingleton(new ExampleClass("innna"));
			var result = PrepareUnit(serviceDescriptors);
			ExampleClass klasa = result.ServiceProvicer.GetService<ExampleClass>()!;
			TestType<ExampleClass>(result);
		}

		[Test]
		public void TransistientRegisterFactory()
		{
			serviceDescriptors.AddTransient<IExampleInterface>(sp =>
			{
				return new ExampleClass();
			});
			var result = PrepareUnit(serviceDescriptors);
			TestType<IExampleInterface>(result);
		}


		[Test]
		public void TransistientKeyType()
		{
			serviceDescriptors.AddTransient<IExampleInterface, ExampleClass>();
			var result = PrepareUnit(serviceDescriptors);
			TestType<IExampleInterface>(result);
		}

		private static void TestType<T>(TestResult result)
		{
			var obj1 = result.Unity.Resolve<T>(); ;
			Assert.That(obj1, Is.Not.Null, $"Nie działa unity {typeof(T)}");
			var obj2 = result.ServiceProvicer.GetService<T>();
			Assert.That(obj1, Is.Not.Null, $"Nie działa serviceProver {typeof(T)}");
		}

		private TestResult PrepareUnit(IServiceCollection serviceDescriptors)
		{
			TestResult testResut = new TestResult(this.service, serviceDescriptors);
			Assert.That(testResut.Unity, Is.Not.Null);
			Assert.That(testResut.ServiceProvicer, Is.Not.Null);
			return testResut;
		}

	}
}