namespace Dme.Persistence.Models.Models;

public class UserEntity
{
	public int Id { get; set; }

	public int NameId { get; set; }
	public NameEntity Name { get; set; } = null!;

	public DateTimeOffset BirthDate { get; set; }
	public DateTimeOffset RegisteredAt { get; set; }

	public string? Email { get; set; }

	public ICollection<DocumentEntity>? Documents { get; set; }
	public ICollection<PictureEntity>? Pictures { get; set; }
}