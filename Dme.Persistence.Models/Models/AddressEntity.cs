namespace Dme.Persistence.Models.Models;

public class AddressEntity
{
	public int Id { get; set; }

	public int StreetNumber { get; set; }
	public string StreetName { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Country { get; set; }
	public string Postcode { get; set; }
	public string Timezone { get; set; }
}