using System.Collections.Generic;
using Newtonsoft.Json;

namespace OrderCalculator.Domain
{
	public class Book
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("authors")]
		public List<string> Authors { get; set; }
		[JsonProperty("price")]
		public decimal Price { get; set; }
		[JsonProperty("genre")]
		public Genre Genre { get; set; }
	}
}
