using Dme.Interaction.Models.Users;
using Dme.Persistence.Models.Models;

namespace Dme.Persistence.Mappers;

public static class UserMapper
{
	public static UserEntity ToPersistence(this User user)
	{
		var documents = new List<DocumentEntity>();
		if (user.Document != null)
			documents.Add(user.Document.ToPersistence());

		return new UserEntity
		{
			Email = user.Email ?? string.Empty,
			BirthDate = user.BirthDate,
			Documents = documents,
			Name = user.Name.ToPersistence(),
			Pictures = new List<PictureEntity> {user.Picture.ToPersistence()},
			RegisteredAt = user.RegisteredAt
		};
	}
}