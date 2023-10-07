namespace Dme.Persistence.Models.Models;

public class NameEntity
{
	public int Id { get; set; }
	
	public int TitleId { get; set; }
	public TitleEntity? Title { get; set; }

	public string First { get; set; } = null!;
	public string Last { get; set; } = null!;
}