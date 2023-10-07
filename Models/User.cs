using System.ComponentModel.DataAnnotations;

namespace SIMPOS.Models;

public class User
{
	public int UserId { get; set; }
	public string UID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
}

