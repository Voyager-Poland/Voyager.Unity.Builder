
namespace Voyager.Unity.Builder.Test
{
	public class Tests
	{
		private UnityBuilder service;

		[SetUp]
		public void Setup()
		{
			service = new Voyager.Unity.Builder.UnityBuilder();
		}

		[Test]
		public void EmptyServicCollection()
		{
			var unity = service.CreateBuilder(null);
			Assert.That(unity, Is.Not.Null);
			var provider = service.CreateServiceProvider(unity);
			Assert.That(provider, Is.Not.Null);
		}
	}
}