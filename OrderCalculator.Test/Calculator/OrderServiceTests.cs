using System.Collections.Generic;
using System.Linq;
using OrderCalculator.Calculator;
using OrderCalculator.DataService;
using OrderCalculator.Logging;
using Moq;
using Xunit;
using OrderCalculator.Contracts.Request;
using OrderCalculator.Domain;

namespace OrderCalculator.Test.Calculator
{
	public class OrderServiceTests
	{
		[Fact]
		public void TestOrderCalculator_BookNotFound()
		{
			// Arrange
			var books = SetupTestBooks();
			var fees = SetupTestFees();
			var mockRepo = ConfigureeepositoryQueryMock(books, fees);
			var mockLogger = new Mock<ILogger>();
			var calculator = new OrderService(mockLogger.Object, mockRepo.Object);

			var book = books.First();
			var order = new Order
			{
				BookOrders = new List<Order.BookOrder>
				{
					new Order.BookOrder { Title = "Not found", Authors = new List<string>{ "Invalid"}, Units = 1 },
					new Order.BookOrder { Title = book.Name, Authors = book.Authors, Units = 3 }
				}
			};
			// Act
			var result = calculator.CalculateOrderTotal(order);
			// Assert
			mockLogger.Verify(l => l.LogWarning("Book not found: Not found by Invalid"));
			mockLogger.VerifyNoOtherCalls();
			Assert.Equal(book.Price * 3 * (1 + fees.Tax), result.TotalWithTax);
			Assert.Equal(book.Price * 3, result.TotalWithoutTax);
		}

		[Fact]
		public void TestOrderCalculator_Discount()
		{
			// Arrange
			var books = SetupTestBooks();
			var fees = SetupTestFees();
			var discounts = new List<Discount> { new Discount { Genre = Genre.Fantasy, Multiplier = 0.9m } };
			var mockRepo = ConfigureeepositoryQueryMock(books, fees, discounts);
			var mockLogger = new Mock<ILogger>();
			var calculator = new OrderService(mockLogger.Object, mockRepo.Object);

			var order = new Order
			{
				BookOrders = new List<Order.BookOrder>
				{
					new Order.BookOrder { Title = books[0].Name, Authors = books[0].Authors, Units = 2},
					new Order.BookOrder { Title = books[1].Name, Authors = books[1].Authors, Units = 1 },
					new Order.BookOrder { Title = books[2].Name, Authors = books[2].Authors, Units = 4 },
				}
			};
			// Act
			var result = calculator.CalculateOrderTotal(order);
			// Assert
			mockLogger.VerifyNoOtherCalls();
			var cost = (books[0].Price * 2) + books[1].Price + (books[2].Price * 0.1m * 4);
			Assert.Equal(cost * (1 + fees.Tax), result.TotalWithTax);
			Assert.Equal(cost, result.TotalWithoutTax);
		}

		[Fact]
		public void TestOrderCalculator_DeliveryFee()
		{
			// Arrange
			var books = SetupTestBooks();
			var fees = SetupTestFees();
			var mockRepo = ConfigureeepositoryQueryMock(books, fees);
			var mockLogger = new Mock<ILogger>();
			var calculator = new OrderService(mockLogger.Object, mockRepo.Object);

			var order = new Order
			{
				BookOrders = new List<Order.BookOrder>
				{
					new Order.BookOrder { Title = books[0].Name, Authors = books[0].Authors, Units = 1}
				}
			};
			// Act
			var result = calculator.CalculateOrderTotal(order);
			// Assert
			mockLogger.VerifyNoOtherCalls();
			var cost = books[0].Price + fees.DeliveryFees.First().Fee;
			Assert.Equal(cost * (1 + fees.Tax), result.TotalWithTax);
			Assert.Equal(cost, result.TotalWithoutTax);
		}

		private Mock<IRepositoryQuery> ConfigureeepositoryQueryMock(List<Book> books, Fees fees, List<Discount> discounts = null)
		{
			var mockRepo = new Mock<IRepositoryQuery>();
			books.ForEach(b => mockRepo.Setup(r => r.GetBook(b.Name, b.Authors)).Returns(b));
			mockRepo.Setup(r => r.GetActiveFees()).Returns(fees);
			discounts?.ForEach(d => mockRepo.Setup(r => r.GetActiveDiscount(d.Genre)).Returns(d));
			return mockRepo;
		}

		private List<Book> SetupTestBooks()
		{
			return new()
			{
				new Book { Name = "Gaudy Night", Authors = new () { "Dorothy L. Sayers" }, Genre = Genre.Crime, Price = 10.5m },
				new Book { Name = "Anna Karenina", Authors = new() { "Leo Tolstoy" }, Genre = Genre.Romance, Price = 22.4m },
				new Book { Name = "Sword of Destiny", Authors = new() { "Andrzej Sapkowski" }, Genre = Genre.Fantasy, Price = 19.6m },
			};
		}

		private Fees SetupTestFees()
		{
			return new Fees
			{
				Tax = 0.15m,
				DeliveryFees = new()
				{
					new Fees.DeliveryFee { Threshold = 25, Fee = 6.5m }
				}
			};
		}
	}
}
