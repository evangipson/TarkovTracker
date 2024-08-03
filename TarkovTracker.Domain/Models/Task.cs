namespace TarkovTracker.Domain.Models
{
	public class Task
	{
		public string Name { get; set; } = "";

		public List<Objective> Objectives { get; set; } = [];

		public List<Map> Maps { get; set; } = [];

		public Trader? Trader { get; set; }
	}
}
