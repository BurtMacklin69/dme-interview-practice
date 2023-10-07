namespace Dme.Persistence.Models.Models;

public class PictureEntity
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public string Large { get; set; } = null!;
	public string Medium { get; set; } = null!;
	public string Thumbnail { get; set; } = null!;
}