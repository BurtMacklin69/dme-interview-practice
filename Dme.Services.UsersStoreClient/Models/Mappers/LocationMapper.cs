using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers;

internal static class LocationMapper
{
	public static Location? ToInteraction(this LocationDto? location)
	{
		if (location == null) return null;
		return new Location
		{
			City = location.City ?? string.Empty,
			Coordinates = location.Coordinates?.Map(),
			Country = location.Country ?? string.Empty,
			Postcode = location.Postcode ?? string.Empty,
			State = location.State ?? string.Empty,
			Street = location.Street?.Map(),
			Timezone = location.Timezone?.Offset ?? string.Empty
		};
	}
}