namespace Dme.Persistence.Models.Models;

public class PictureEntity
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public string Large { get; set; }
	public string Medium { get; set; }
	public string Thumbnail { get; set; }
}