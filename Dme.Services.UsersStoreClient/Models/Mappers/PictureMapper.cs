using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers;

internal static class PictureMapper
{
	public static Picture ToInteraction(this PictureDto picture) =>
		new()
		{
			Large = picture.Large ?? throw new NullReferenceException("Expected picture with large data"),
			Medium = picture.Medium ?? throw new NullReferenceException("Expected picture with Medium data"),
			Thumbnail = picture.Thumbnail ?? throw new NullReferenceException("Expected picture with Thumbnail data"),
		};
}