namespace OrderCalculator.Logging
{
	public class Logger : ILogger
	{
		public void LogError(string error)
		{
			System.Console.WriteLine($"ERROR - {error}");
		}

		public void LogWarning(string warning)
		{
			System.Console.WriteLine($"Warn - {warning}");
		}

		public void LogString(string message)
		{
			System.Console.Write(message);
		}
	}
}
