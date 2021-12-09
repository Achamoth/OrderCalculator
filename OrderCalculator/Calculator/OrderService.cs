using System.Linq;
using OrderCalculator.Contracts.Request;
using OrderCalculator.Contracts.Response;
using OrderCalculator.DataService;
using OrderCalculator.Domain;
using OrderCalculator.Logging;

namespace OrderCalculator.Calculator
{
	public class OrderService : IOrderService
	{
		private readonly ILogger _logger;
		private readonly IRepositoryQuery _repository;

		public OrderService(ILogger logger, IRepositoryQuery reposistory)
		{
			_logger = logger;
			_repository = reposistory;
		}

		public OrderTotal CalculateOrderTotal(Order order)
		{
			var result = new OrderTotal();
			var fees = _repository.GetActiveFees();
			order.BookOrders.ForEach(b =>
			{
				var book = _repository.GetBook(b.Title, b.Authors);
				if (book == null)
					_logger.LogWarning($"Book not found: {b.Title} by {b.Authors.FirstOrDefault()}");
				else
					UpdateTotals(result, book, fees, b.Units);
			});
			AdjustForDeliveryFees(result, fees);
			return result;
		}

		private void UpdateTotals(OrderTotal totals, Book book, Fees fees, int units)
		{
			var bookCost = book.Price * units;
			var genreDiscount = _repository.GetActiveDiscount(book.Genre);
			if (genreDiscount != null) {
				bookCost -= bookCost * genreDiscount.Multiplier;
			}
			totals.TotalWithoutTax += bookCost;
			totals.TotalWithTax += (bookCost + bookCost * fees.Tax);
		}

		private static void AdjustForDeliveryFees(OrderTotal orderTotal, Fees fees)
		{
			// Assuming that the delivery fee threshold is based on post-tax cost and that delivery fee has GST
			var applicableDeliveryFee = fees.DeliveryFees
				.Where(f => f.Threshold >= orderTotal.TotalWithTax)
				.OrderBy(f => f.Threshold)
				.FirstOrDefault();
			if (applicableDeliveryFee != null) {
				orderTotal.TotalWithoutTax += applicableDeliveryFee.Fee;
				orderTotal.TotalWithTax += (applicableDeliveryFee.Fee + applicableDeliveryFee.Fee * fees.Tax);
			}
		}
	}
}
