using Dme.Interaction.Models.Users;
using Dme.Persistence.Models.Models;

namespace Dme.Persistence.Mappers;

public static class PictureMapper
{
	public static PictureEntity ToPersistence(this Picture picture) =>
		new()
		{
			Large = picture.Large,
			Medium = picture.Medium,
			Thumbnail = picture.Thumbnail
		};

	public static Picture ToInteraction(this PictureEntity entity) =>
		new()
		{
			Large = entity.Large,
			Medium = entity.Medium,
			Thumbnail = entity.Thumbnail
		};
}