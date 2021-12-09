using System.Collections.Generic;
using OrderCalculator.Domain;
using OrderCalculator.Logging;
using OrderCalculator.Utils;

namespace OrderCalculator.DataService
{
	public class DataLoader : IDataLoader
	{
		// I would put this in some sort of appsettings
		public const string BookFileName = "books.json";
		public const string DiscountFileName = "discounts.json";
		public const string FeesFileName = "fees.json";

		private readonly ILogger _logger;

		public DataLoader(ILogger logger)
		{
			_logger = logger;
		}

		public IRepositoryQuery BuildRepository(string directory)
		{
			var result = new Repository();

			var books = JsonUtils.DeserializeJson<List<Book>>($"{directory}/{BookFileName}", _logger);
			books.ForEach(b => result.AddBook(b));

			var discounts = JsonUtils.DeserializeJson<List<Discount>>($"{directory}/{DiscountFileName}",  _logger);
			discounts.ForEach(d => result.AddDiscount(d));

			var fees = JsonUtils.DeserializeJson<Fees>($"{directory}/{FeesFileName}", _logger);
			result.ConfigureFees(fees);

			return result;
		}
	}
}
