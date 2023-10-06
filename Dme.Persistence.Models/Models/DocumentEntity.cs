namespace Dme.Persistence.Models.Models;

public class DocumentEntity
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public int DocumentTypeId { get; set; }

	public string Value { get; set; }
}