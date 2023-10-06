using Dme.Interaction.Models.Users;

namespace Dme.Services.UsersStoreClient.Models.Mappers
{
	internal static class UserMapper
	{
		public static User ToInteraction(this UserDto user) =>
			new()
			{
				Email = user.Email ?? string.Empty,
				BirthDate = user.Dob.Date,
				CellPhone = user.Cell ?? string.Empty,
				Document = user.Id.ToInteraction(),
				Name = user.Name.ToInteraction(),
				Gender = user.Gender ?? string.Empty,
				Location = user.Location?.ToInteraction(),
				Picture = user.Picture.ToInteraction(),
				Nationality = user.Nat ?? string.Empty,
				Phone = user.Phone ?? string.Empty,
				RegisteredAt = user.Registered.Date
			};
	}
}
