using Dme.Persistence.Models.Models;
using Document = Dme.Interaction.Models.Users.Document;

namespace Dme.Persistence.Mappers;

public static class DocumentMapper
{
	public static DocumentEntity? ToPersistence(this Document doc)
	{
		if (string.IsNullOrWhiteSpace(doc.Name) &&
		    string.IsNullOrWhiteSpace(doc.Value))
			return null;

		return new DocumentEntity
		{
			Value = doc.Value,
			Type = doc.Name
		};
	}

	public static Document ToInteraction(this DocumentEntity entity) =>
		new()
		{
			Name = entity.Type,
			Value = entity.Value
		};
}