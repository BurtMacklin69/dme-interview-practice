using Dme.Interaction.Models.Users;
using Dme.Persistence.Models.Models;

namespace Dme.Persistence.Mappers;

public static class NameMapper
{
	public static NameEntity ToPersistence(this Name name)
	{
		if (string.IsNullOrWhiteSpace(name.First) &&
		    string.IsNullOrWhiteSpace(name.Last))
			throw new ArgumentException("Name expected as not empty", nameof(name));

		return new NameEntity
		{
			First = name.First,
			Last = name.Last,
			Title = name.Title
		};
	}

	public static Name ToInteraction(this NameEntity entity) => new()
	{
		First = entity.First,
		Last = entity.Last,
		Title = entity.Title
	};
}