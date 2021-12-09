namespace OrderCalculator.DataService
{
	public interface IDataLoader
	{
		public IRepositoryQuery BuildRepository(string dataDirectory);
	}
}
