namespace Dme.Interaction.Models.Users;

public class User
{
	public string? Gender { get; set; }

	public Name Name { get; set; } = null!;

	public Location? Location { get; set; }

	public string? Email { get; set; }

	public DateTimeOffset BirthDate { get; set; }

	public DateTimeOffset RegisteredAt { get; set; }

	public string? Phone { get; set; }

	public string? CellPhone { get; set; }

	public Document Document { get; set; } = null!;

	public Picture Picture { get; set; } = null!;

	public string? Nationality { get; set; }
}