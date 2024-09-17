
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

			Assert.Pass();
		}
	}
}