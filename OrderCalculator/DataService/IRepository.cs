using System.Collections.Generic;
using OrderCalculator.Domain;


namespace OrderCalculator.DataService
{
	public interface IRepositoryQuery
	{
		public Book GetBook(string name, IEnumerable<string> authors);
		public Discount GetActiveDiscount(Genre genre);
		public Fees GetActiveFees();
	}

	public interface IRepositoryCommand
	{
		public void AddBook(Book book);
		public void AddDiscount(Discount discount);
		public void ConfigureFees(Fees fees);
	}
}
