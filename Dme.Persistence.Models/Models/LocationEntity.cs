namespace Dme.Persistence.Models.Models;

public class LocationEntity
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public int AddressId { get; set; }
	public int CoordinatesId { get; set; }
}