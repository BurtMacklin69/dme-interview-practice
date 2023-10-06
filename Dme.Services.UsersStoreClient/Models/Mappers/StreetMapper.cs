using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers;

internal static class StreetMapper
{
	public static Street? Map(this StreetDto? street)
	{
		if (street?.Number == null)
			return null;

		return new Street
		{
			Name = street.Name ?? string.Empty,
			Number = street.Number.Value
		};
	}
}