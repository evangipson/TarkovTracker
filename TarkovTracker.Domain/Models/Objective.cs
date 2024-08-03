namespace TarkovTracker.Domain.Models
{
	public class Objective
	{
		public string Description { get; set; } = "";

		public List<Map> Maps { get; set; } = [];

		public List<Item> RequiredItems { get; set; } = [];
	}
}
