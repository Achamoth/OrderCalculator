using System.Collections.Generic;
using Newtonsoft.Json;

namespace OrderCalculator.Contracts.Request
{
	public class Order
	{
		[JsonProperty("bookOrders")]
		public List<BookOrder> BookOrders { get; set; }
		
		public class BookOrder
		{
			[JsonProperty("title")]
			public string Title { get; set; }
			[JsonProperty("authors")]
			public List<string> Authors { get; set; }
			[JsonProperty("units")]
			public int Units { get; set; }
		}
	}
}
