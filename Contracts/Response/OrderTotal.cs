namespace OrderCalculator.Contracts.Response
{
	public class OrderTotal
	{
		public decimal TotalWithTax { get; set; }
		public decimal TotalWithoutTax { get; set; }
	}
}
