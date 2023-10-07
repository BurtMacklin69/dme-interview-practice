namespace Dme.Persistence.Models.Models;

public class DocumentEntity
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public string Type { get; set; } = null!;

	public string Value { get; set; } = null!;
}