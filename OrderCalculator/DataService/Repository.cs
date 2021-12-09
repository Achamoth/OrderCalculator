using System.Collections.Generic;
using System.Linq;
using OrderCalculator.Domain;


namespace OrderCalculator.DataService
{
	public class Repository : IRepository
	{
		private readonly Dictionary<string, Book> _books;
		private readonly Dictionary<Genre, Discount> _discounts;
		private Fees _fees;

		public Repository()
		{
			_books = new Dictionary<string, Book>();
			_discounts = new Dictionary<Genre, Discount>();
		}

		public void AddBook(Book book)
		{
			var bookKey = GenerateBookKey(book.Name, book.Authors);
			if (!_books.ContainsKey(bookKey))
			{
				_books.Add(bookKey, book);
			}
		}

		public void AddDiscount(Discount discount)
		{
			// Assume no genre can have more than one active discount
			if (!_discounts.ContainsKey(discount.Genre))
			{
				_discounts.Add(discount.Genre, discount);
			}
		}

		public void ConfigureFees(Fees fees)
		{
			_fees = fees;
		}

		#region IRepository

		public Book GetBook(string name, IEnumerable<string> authors)
		{
			if (_books.TryGetValue(GenerateBookKey(name, authors), out var result))
			{
				return result;
			}
			return null;
		}

		public Discount GetActiveDiscount(Genre genre)
		{
			if (_discounts.TryGetValue(genre, out var discount))
			{
				return discount;
			}
			return null;
		}

		public Fees GetActiveFees()
		{
			return _fees;
		}

		#endregion

		private string GenerateBookKey(string title, IEnumerable<string> authors)
		{
			// Best I can do for now; don't want to assume titles are unique. I'd rather use ISBN.
			return $"{title.ToLower()}-{string.Join("-", authors.OrderBy(a => a).Select(a => a.ToLower()))}";
		}
	}
}
