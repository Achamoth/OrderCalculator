using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrderCalculator.Domain
{
	public class Fees
	{
		[JsonProperty("tax")]
		public decimal Tax { get; set; }
		[JsonProperty("deliveryFees")]
		public List<DeliveryFee> DeliveryFees { get; set; }

		public class DeliveryFee
		{
			[JsonProperty("threshold")]
			public int Threshold { get; set; }
			[JsonProperty("fee")]
			public decimal Fee { get; set; }
		}
	}
}
