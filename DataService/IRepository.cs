using System.Collections.Generic;
using OrderCalculator.Domain;


namespace OrderCalculator.DataService
{
	public interface IRepository
	{
		public Book GetBook(string name, IEnumerable<string> authors);
		public Discount GetActiveDiscount(Genre genre);
		public Fees GetActiveFees();
	}
}
