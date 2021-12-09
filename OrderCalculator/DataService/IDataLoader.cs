namespace OrderCalculator.DataService
{
	public interface IDataLoader
	{
		public IRepository BuildRepository(string dataDirectory);
	}
}
