using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers;

internal static class DocumentMapper
{
	public static Document ToInteraction(this IdDto id)
	{
		if (string.IsNullOrWhiteSpace(id.Name) &&
		    string.IsNullOrWhiteSpace(id.Value))
			throw new ArgumentException("Id expected as not empty", nameof(id));

		return new Document
		{
			Name = id.Name,
			Value = id.Value
		};
	}
}