using Newtonsoft.Json;

namespace OrderCalculator.Domain
{
	public class Discount
	{
		[JsonProperty("genre")]
		public Genre Genre { get; set; }
		[JsonProperty("multiplier")]
		public decimal Multiplier { get; set; }
	}
}
