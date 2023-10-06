using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers;

internal static class NameMapper
{
	public static Name ToInteraction(this NameDto name)
	{
		if (string.IsNullOrWhiteSpace(name.First) &&
		    string.IsNullOrWhiteSpace(name.Last))
			throw new ArgumentException("Name expected as not empty", nameof(name));

		return new Name
		{
			First = name.First,
			Last = name.Last,
			Title = name.Title ?? string.Empty
		};
	}
}