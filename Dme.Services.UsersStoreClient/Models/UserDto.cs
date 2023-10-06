namespace Dme.Services.UsersStoreClient.Models;

internal class UserDto
{
	public string? Gender { get; set; }

	public NameDto Name { get; set; } = null!;

	public LocationDto? Location { get; set; }

	public string? Email { get; set; }

	// будет проигнорировано - см. readme
	public LoginDto? Login { get; set; }

	public BirthDateDto Dob { get; set; } = null!;

	public RegisteredDto Registered { get; set; } = null!;

	public string? Phone { get; set; }

	public string? Cell { get; set; }

	public IdDto Id { get; set; } = null!;

	public PictureDto Picture { get; set; } = null!;

	public string? Nat { get; set; }
}