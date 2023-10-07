using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers;

internal static class UserMapper
{
	public static User ToInteraction(this UserDto user) =>
		new()
		{
			Email = user.Email ?? string.Empty,
			BirthDate = user.Dob.Date,
			Document = user.Id?.ToInteraction() ?? null,
			Name = user.Name.ToInteraction(),
			Picture = user.Picture.ToInteraction(),
			RegisteredAt = user.Registered.Date
		};
}