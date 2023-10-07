namespace Dme.Services.UsersStoreClient.Models;

internal class UserDto
{
	public NameDto Name { get; set; } = null!;

	public string? Email { get; set; }

	public BirthDateDto Dob { get; set; } = null!;

	public RegisteredDto Registered { get; set; } = null!;

	public IdDto? Id { get; set; }

	public PictureDto Picture { get; set; } = null!;
}