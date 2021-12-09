namespace OrderCalculator.Logging
{
	public interface ILogger
	{
		public void LogError(string error);
		public void LogWarning(string warning);
		public void LogString(string message);

	}
}
