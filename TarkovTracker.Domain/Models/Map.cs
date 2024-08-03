namespace TarkovTracker.Domain.Models
{
	public class Map
	{
		public string Name { get; set; } = "";

		public List<MapCallout> Callouts { get; set; } = [];
	}
}
