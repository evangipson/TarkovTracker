namespace TarkovTracker.Domain.Models
{
	public class MapCallout
	{
		public string Name { get; set; } = "";

		public List<MapArea>? Location { get; set; }
	}

	public enum MapArea
	{
		West,
		East,
		North,
		South,
		Middle,
		Underground,
	}
}
