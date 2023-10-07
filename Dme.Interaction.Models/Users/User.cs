namespace Dme.Interaction.Models.Users;

public class User
{
	public Name Name { get; set; } = null!;

	public string? Email { get; set; }

	public DateTimeOffset BirthDate { get; set; }

	public DateTimeOffset RegisteredAt { get; set; }

	public Document? Document { get; set; }

	public Picture Picture { get; set; } = null!;
}