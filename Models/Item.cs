using System.ComponentModel.DataAnnotations;

namespace SIMPOS.Models;

public class Item
{
	public int ItemId { get; set; }
	public string Type { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
}

