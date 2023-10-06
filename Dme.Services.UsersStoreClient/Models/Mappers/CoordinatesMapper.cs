using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers;

internal static class CoordinatesMapper
{
	public static Coordinates? Map(this CoordinatesDto? coordinates)
	{
		if (coordinates == null) return null;
		return new Coordinates
		{
			Latitude = float.Parse(coordinates.Latitude),
			Longitude = float.Parse(coordinates.Longitude)
		};
	}
}