using System.ComponentModel.DataAnnotations;

namespace SIMPOS.Models;

public class Items
{
	public int ItemId { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
}

