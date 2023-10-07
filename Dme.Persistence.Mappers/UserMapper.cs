using Dme.Interaction.Models.Users;
using Dme.Persistence.Models.Models;

namespace Dme.Persistence.Mappers;

public static class UserMapper
{
	public static UserEntity ToPersistence(this User user)
	{
		var documents = new List<DocumentEntity>();
		if (user.Document != null)
		{
			var document = user.Document!.ToPersistence();
			if (document != null)
				documents.Add(document);
		}

		return new UserEntity
		{
			Id = user.Id,
			Email = user.Email ?? string.Empty,
			BirthDate = user.BirthDate,
			Documents = documents,
			Name = user.Name.ToPersistence(),
			Pictures = new List<PictureEntity> {user.Picture.ToPersistence()},
			RegisteredAt = user.RegisteredAt
		};
	}
	public static User ToInteraction(this UserEntity user)
	{
		return new User
		{
			Id = user.Id,
			Email = user.Email ?? string.Empty,
			BirthDate = user.BirthDate,
			Document = user.Documents?.MaxBy(d => d.Id)?.ToInteraction(),
			Name = user.Name.ToInteraction(),
			Picture = user.Pictures!.MaxBy(d => d.Id)!.ToInteraction(),
			RegisteredAt = user.RegisteredAt
		};
	}
}