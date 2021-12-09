using OrderCalculator.Contracts.Request;
using OrderCalculator.Contracts.Response;

namespace OrderCalculator.Calculator
{
	public interface IOrderService
	{
		public OrderTotal CalculateOrderTotal(Order order);
	}
}
