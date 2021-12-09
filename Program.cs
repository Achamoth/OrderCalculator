using OrderCalculator.Contracts.Request;
using OrderCalculator.Calculator;
using OrderCalculator.DataService;
using OrderCalculator.Logging;
using OrderCalculator.Utils;

namespace OrderCalculator
{
	class Program
	{
		static void Main()
		{
			var logger = new Logger();
			var repository = new DataLoader(logger).BuildRepository("Data");
			var order = JsonUtils.DeserializeJson<Order>("order.json",  logger);
			var total = new OrderService(logger, repository).CalculateOrderTotal(order);
			logger.LogString(JsonUtils.SerializeJson(total));
		}
	}
}
