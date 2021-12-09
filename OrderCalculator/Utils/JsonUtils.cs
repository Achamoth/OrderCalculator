using System.IO;
using Newtonsoft.Json;
using OrderCalculator.Logging;

namespace OrderCalculator.Utils
{
	public static class JsonUtils
	{
		public static T DeserializeJson<T>(string pathname, ILogger logger)
		{
			// I would add more error handling
			if (!File.Exists(pathname))
			{
				logger.LogError($"File not found at path {pathname}");
			}
			var json = File.ReadAllText(pathname);
			return JsonConvert.DeserializeObject<T>(json);
		}

		public static string SerializeJson<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
