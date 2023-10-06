namespace Dme.Interaction.Models.Users;

public class Location
{
	public Street? Street { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Country { get; set; }
	public string Postcode { get; set; }
	public Coordinates? Coordinates { get; set; }
	public string Timezone { get; set; }
}