namespace Dme.Interaction.Models.Users;

public class User
{
	public Id Id { get; init; } = Id.NotSetYet;

	public Name Name { get; init; } = null!;

	public string? Email { get; init; }

	public DateTimeOffset BirthDate { get; init; }

	public DateTimeOffset RegisteredAt { get; init; }

	public Document? Document { get; init; }

	public Picture Picture { get; init; } = null!;
}