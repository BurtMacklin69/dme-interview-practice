namespace Dme.Services.UsersStoreClient.Models;

internal class LocationDto
{
	public StreetDto? Street { get; set; }
	public string? City { get; set; }
	public string? State { get; set; }
	public string? Country { get; set; }
	public string? Postcode { get; set; }
	public CoordinatesDto? Coordinates { get; set; }
	public TimezoneDto? Timezone { get; set; }
}