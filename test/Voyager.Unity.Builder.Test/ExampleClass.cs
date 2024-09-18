namespace Voyager.Unity.Builder.Test
{
	public class ExampleClass : IExampleInterface, IDisposable
	{
		internal static bool anyDisposed = false;

		public ExampleClass() { }
		public ExampleClass(string jakisParam)
		{
			JakisParam = jakisParam;
		}

		public string JakisParam { get; }

		public void Dispose()
		{
			anyDisposed = true;
		}
	}
}