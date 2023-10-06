namespace Dme.Persistence.Models.Models;

public class ContactEntity
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public string Phone { get; set; }
	public string CellPhone { get; set; }
	public string Email { get; set; }
}